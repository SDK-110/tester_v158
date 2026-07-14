using System;

namespace testapp.mylib.canopen.services
{
    /// <summary>Heartbeat consumer — monitors a slave node's heartbeat messages.</summary>
    internal class HeartbeatConsumer : IRoutableService
    {
        private readonly byte _nodeId;
        private DateTime _lastHeartbeat;
        private bool _wasTimedOut;
        private bool _hadFirstHeartbeat;

        public int HeartbeatTimeoutMs { get; set; } = 3000;
        public bool IsAlive => _hadFirstHeartbeat && !_wasTimedOut;
        /// <summary>Last NMT state received via heartbeat.</summary>
        public NMTState LastState { get; private set; } = NMTState.Unknown;

        public event EventHandler Lost;
        public event EventHandler Restored;
        /// <summary>Fires when a heartbeat with a different NMT state arrives.</summary>
        public event EventHandler<NMTState> StateChanged;

        public HeartbeatConsumer(byte nodeId, FrameRouter router)
        {
            _nodeId = nodeId;
            _lastHeartbeat = DateTime.MinValue;
            router.Subscribe(CANopenID.Heartbeat(nodeId), this);
        }

        void IRoutableService.HandleFrame(CanFrame frame)
        {
            _lastHeartbeat = DateTime.Now;
            _hadFirstHeartbeat = true;

            if (_wasTimedOut)
            {
                _wasTimedOut = false;
                Restored?.Invoke(this, EventArgs.Empty);
            }

            if (frame.Data != null && frame.Data.Length >= 1)
            {
                NMTState state = (NMTState)frame.Data[0];
                if (state != LastState)
                {
                    LastState = state;
                    StateChanged?.Invoke(this, state);
                }
            }
        }

        /// <summary>Call periodically (e.g. every 100ms) from master timer.</summary>
        public void CheckTimeout()
        {
            if (!_hadFirstHeartbeat) return;
            if (_wasTimedOut) return;

            if ((DateTime.Now - _lastHeartbeat).TotalMilliseconds > HeartbeatTimeoutMs)
            {
                _wasTimedOut = true;
                LastState = NMTState.Unknown;
                Lost?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
