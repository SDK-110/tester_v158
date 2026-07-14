using System;
using System.Collections.Generic;
using System.Threading;
using testapp.mylib.canopen.eds;

namespace testapp.mylib.canopen
{
    /// <summary>CANopen master — manages SLCAN transport, remote nodes, and heartbeat monitoring.</summary>
    public class CanOpenMaster : IDisposable
    {
        private readonly ISLCANTransport _transport;
        private readonly FrameRouter _router;
        private readonly Dictionary<byte, RemoteNode> _nodes = new Dictionary<byte, RemoteNode>();
        private Timer _heartbeatTimer;
        private readonly object _lock = new object();
        private bool _running;

        public bool IsRunning => _running;
        public IReadOnlyDictionary<byte, RemoteNode> Nodes =>
            new Dictionary<byte, RemoteNode>(_nodes);

        /// <summary>Fires when any node sends an emergency message.</summary>
        public event EventHandler<RemoteNode.EmergencyData> EmergencyReceived;
        /// <summary>Fires when any node's heartbeat status changes.</summary>
        public event EventHandler<HeartbeatEvent> HeartbeatStatusChanged;

        /// <summary>Create master with a serial COM port SLCAN transport.</summary>
        public CanOpenMaster(string comPort, int baudRate = 500000)
            : this(new SLCANSerialPortTransport(comPort, baudRate))
        { }

        /// <summary>Create master with a custom transport (e.g. VirtualSLCANTransport for testing).</summary>
        public CanOpenMaster(ISLCANTransport transport)
        {
            _transport = transport ?? throw new ArgumentNullException(nameof(transport));
            _router = new FrameRouter(_transport);
        }

        /// <summary>Open transport, start heartbeat monitor.</summary>
        public void Start()
        {
            if (_running) return;

            try { _transport.Open(); } catch (Exception ex)
            {
                throw new TransportException("Failed to open CAN transport", ex);
            }

            _running = true;

            // Wire transport receive events into frame router
            _transport.FrameReceived += (s, frame) => _router.Dispatch(frame);

            // Heartbeat timer: check all nodes every 100ms
            _heartbeatTimer = new Timer(_ =>
            {
                lock (_lock)
                {
                    foreach (var node in _nodes.Values)
                        node.Heartbeat?.CheckTimeout();
                }
            }, null, 0, 100);
        }

        /// <summary>Close transport, stop all timers.</summary>
        public void Stop()
        {
            _running = false;
            _heartbeatTimer?.Dispose();
            _heartbeatTimer = null;
            try { _transport.Close(); } catch { }
        }

        /// <summary>Register a remote slave node.</summary>
        public RemoteNode RegisterNode(byte nodeId, string name = null, string edsPath = null)
        {
            if (nodeId < 1 || nodeId > 127)
                throw new ArgumentException("Node ID must be 1-127", nameof(nodeId));

            ObjectDictionary od = null;
            if (!string.IsNullOrEmpty(edsPath))
            {
                od = EDSParser.LoadFromFile(edsPath);
            }

            lock (_lock)
            {
                if (_nodes.ContainsKey(nodeId))
                    throw new InvalidOperationException($"Node {nodeId} already registered");

                var node = new RemoteNode(nodeId, name ?? $"Node_{nodeId}", od, _router);
                _nodes[nodeId] = node;

                node.Emergency += (s, e) => EmergencyReceived?.Invoke(s, e);
                node.HeartbeatLost += (s, e) =>
                    HeartbeatStatusChanged?.Invoke(s, new HeartbeatEvent(nodeId, false));
                node.HeartbeatRestored += (s, e) =>
                    HeartbeatStatusChanged?.Invoke(s, new HeartbeatEvent(nodeId, true));

                return node;
            }
        }

        public void UnregisterNode(byte nodeId)
        {
            lock (_lock)
            {
                if (_nodes.TryGetValue(nodeId, out var node))
                {
                    node.Dispose();
                    _nodes.Remove(nodeId);
                }
            }
        }

        public RemoteNode GetNode(byte nodeId)
        {
            lock (_lock)
            {
                if (_nodes.TryGetValue(nodeId, out var node))
                    return node;
                throw new NodeNotRegisteredException(nodeId);
            }
        }

        public void NMTBroadcast(NMTCommand cmd)
        {
            var frame = CanFrame.NMTCommand(cmd, 0);
            _router.Send(frame);
        }

        public void SendSync()
        {
            _router.Send(CanFrame.SYNC());
        }

        public void SendRawFrame(uint cobId, byte[] data, bool isExtended = false, bool isRemote = false)
        {
            _router.Send(new CanFrame(cobId, data, isRemote, isExtended));
        }

        public void SubscribeRaw(uint cobIdStart, uint cobIdEnd, Action<CanFrame> handler)
        {
            _router.SubscribeRaw(cobIdStart, cobIdEnd, handler);
        }

        public void Dispose()
        {
            Stop();
            lock (_lock)
            {
                foreach (var node in _nodes.Values)
                    node.Dispose();
                _nodes.Clear();
            }
        }
    }

    /// <summary>Heartbeat status change event data.</summary>
    public class HeartbeatEvent : EventArgs
    {
        public byte NodeId { get; }
        public bool IsAlive { get; }
        public HeartbeatEvent(byte nodeId, bool alive) { NodeId = nodeId; IsAlive = alive; }
    }
}
