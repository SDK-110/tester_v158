using System;

namespace testapp.mylib.canopen.services
{
    /// <summary>NMT master — sends NMT commands and tracks slave state.</summary>
    internal class NmtMaster
    {
        private readonly byte _nodeId;
        private readonly FrameRouter _router;
        public NMTState KnownState { get; private set; } = NMTState.Unknown;
        public event EventHandler<NMTState> StateChanged;

        public NmtMaster(byte nodeId, FrameRouter router)
        {
            _nodeId = nodeId;
            _router = router;
        }

        public void SendCommand(NMTCommand cmd)
        {
            var frame = CanFrame.NMTCommand(cmd, _nodeId);
            _router.Send(frame);
            UpdateExpectedState(cmd);
        }

        public void Start() => SendCommand(NMTCommand.Start);
        public void Stop() => SendCommand(NMTCommand.Stop);
        public void EnterPreOp() => SendCommand(NMTCommand.EnterPreOp);
        public void ResetNode() => SendCommand(NMTCommand.ResetNode);
        public void ResetComm() => SendCommand(NMTCommand.ResetComm);

        /// <summary>Update known state from heartbeat or boot-up message.</summary>
        public void UpdateState(NMTState hbState)
        {
            if (KnownState != hbState)
            {
                KnownState = hbState;
                StateChanged?.Invoke(this, hbState);
            }
        }

        private void UpdateExpectedState(NMTCommand cmd)
        {
            NMTState expected;
            switch (cmd)
            {
                case NMTCommand.Start:     expected = NMTState.Operational; break;
                case NMTCommand.Stop:      expected = NMTState.Stopped; break;
                case NMTCommand.EnterPreOp: expected = NMTState.PreOperational; break;
                case NMTCommand.ResetNode:
                case NMTCommand.ResetComm:  expected = NMTState.Initialising; break;
                default: return;
            }
            if (KnownState != expected)
            {
                KnownState = expected;
                StateChanged?.Invoke(this, expected);
            }
        }
    }
}
