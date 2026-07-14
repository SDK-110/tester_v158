using System;
using System.Collections.Generic;

namespace testapp.mylib.canopen
{
    internal interface IRoutableService
    {
        void HandleFrame(CanFrame frame);
    }

    internal class FrameRouter
    {
        private readonly Dictionary<uint, IRoutableService> _routes = new Dictionary<uint, IRoutableService>();
        private readonly List<Tuple<uint, uint, Action<CanFrame>>> _rawSubs =
            new List<Tuple<uint, uint, Action<CanFrame>>>();
        private readonly ISLCANTransport _transport;
        private readonly object _lock = new object();

        public FrameRouter(ISLCANTransport transport)
        {
            _transport = transport ?? throw new ArgumentNullException(nameof(transport));
        }

        public void Subscribe(uint cobId, IRoutableService handler)
        {
            lock (_lock) { _routes[cobId] = handler; }
        }

        public void Unsubscribe(uint cobId)
        {
            lock (_lock) { _routes.Remove(cobId); }
        }

        public void SubscribeRaw(uint cobIdStart, uint cobIdEnd, Action<CanFrame> handler)
        {
            lock (_lock) { _rawSubs.Add(Tuple.Create(cobIdStart, cobIdEnd, handler)); }
        }

        /// <summary>Dispatches a frame for sending (writes to transport).</summary>
        public void Send(CanFrame frame)
        {
            _transport.Send(frame.CobId, frame.Data, frame.IsExtendedFrame, frame.IsRemoteFrame);
        }

        /// <summary>Routes an incoming frame to the right handler by COB-ID.</summary>
        public void Dispatch(CanFrame frame)
        {
            IRoutableService svc;
            lock (_lock)
            {
                if (_routes.TryGetValue(frame.CobId, out svc))
                {
                    svc.HandleFrame(frame);
                    return;
                }
            }

            // Raw catch-all subscriptions
            lock (_lock)
            {
                foreach (var sub in _rawSubs)
                {
                    if (frame.CobId >= sub.Item1 && frame.CobId <= sub.Item2)
                        sub.Item3(frame);
                }
            }
        }

        /// <summary>Wire transport receive event to router dispatch.</summary>
        public void StartReceiveLoop()
        {
            _transport.FrameReceived += (s, f) => Dispatch(f);
        }

        internal static byte ResolveNodeId(uint cobId)
        {
            switch ((cobId >> 7) & 0x0F)
            {
                case 1:  // EMCY: 0x080 + NodeId
                case 3:  // TPDO1
                case 5:  // TPDO2
                case 7:  // TPDO3
                case 9:  // TPDO4
                case 11: // SDO TX
                case 14: // Heartbeat
                    return (byte)(cobId & 0x7F);
                default:
                    return 0;
            }
        }

        internal static byte ResolvePDONumber(uint cobId)
        {
            switch ((cobId >> 7) & 0x0F)
            {
                case 3:  return 1;
                case 5:  return 2;
                case 7:  return 3;
                case 9:  return 4;
                default: return 0;
            }
        }
    }
}
