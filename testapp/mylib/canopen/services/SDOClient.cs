using System;
using System.Threading;
using testapp.mylib.canopen.eds;

namespace testapp.mylib.canopen.services
{
    /// <summary>SDO client — expedited upload/download with retry and timeout.</summary>
    internal class SDOClient : IRoutableService
    {
        private readonly byte _nodeId;
        private readonly FrameRouter _router;
        private readonly int _timeoutMs;
        private readonly int _retryCount = 3;
        private readonly object _lock = new object();
        private AutoResetEvent _responseEvent;
        private CanFrame _response;
        private bool _aborted;

        public ObjectDictionary ObjectDict { get; set; }

        public SDOClient(byte nodeId, FrameRouter router, int timeoutMs = 1000)
        {
            _nodeId = nodeId;
            _router = router;
            _timeoutMs = timeoutMs;
            _responseEvent = new AutoResetEvent(false);
            router.Subscribe(CANopenID.SDOTx(nodeId), this);
        }

        void IRoutableService.HandleFrame(CanFrame frame)
        {
            lock (_lock)
            {
                // SCS=4 means abort (bits 7-5 = 100 = 0x80)
                if ((frame.Data[0] & 0xE0) == 0x80)
                    _aborted = true;
                _response = frame;
                _responseEvent?.Set();
            }
        }

        /// <summary>Expedited SDO upload (read from slave's object dictionary).</summary>
        public byte[] Upload(ushort index, byte subIndex)
        {
            for (int retry = 0; retry < _retryCount; retry++)
            {
                _aborted = false;
                _responseEvent.Reset();

                var request = CanFrame.SDOUploadRequest(_nodeId, index, subIndex);
                _router.Send(request);

                if (_responseEvent.WaitOne(_timeoutMs))
                {
                    lock (_lock)
                    {
                        if (_aborted)
                        {
                            uint code = _response.Data.Length >= 8
                                ? BitConverter.ToUInt32(_response.Data, 4) : 0;
                            throw new SDOException(
                                $"SDO upload aborted: 0x{index:X4}:{subIndex}",
                                index, subIndex, (SDOAbortCode)code);
                        }
                        return ParseUploadResponse(_response.Data);
                    }
                }
            }
            throw new SDOException(
                $"SDO upload timeout: 0x{index:X4}:{subIndex}",
                index, subIndex, SDOAbortCode.GeneralError);
        }

        /// <summary>Expedited SDO download (write to slave's object dictionary).</summary>
        public bool Download(ushort index, byte subIndex, byte[] data)
        {
            for (int retry = 0; retry < _retryCount; retry++)
            {
                _aborted = false;
                _responseEvent.Reset();

                var request = CanFrame.SDODownloadRequest(_nodeId, index, subIndex, data);
                _router.Send(request);

                if (_responseEvent.WaitOne(_timeoutMs))
                {
                    lock (_lock)
                    {
                        if (_aborted)
                        {
                            uint code = _response.Data.Length >= 8
                                ? BitConverter.ToUInt32(_response.Data, 4) : 0;
                            throw new SDOException(
                                $"SDO download aborted: 0x{index:X4}:{subIndex}",
                                index, subIndex, (SDOAbortCode)code);
                        }
                        // SCS=3 (0x60) = download response OK
                        return (_response.Data[0] & 0xE0) == 0x60;
                    }
                }
            }
            throw new SDOException(
                $"SDO download timeout: 0x{index:X4}:{subIndex}",
                index, subIndex, SDOAbortCode.GeneralError);
        }

        public void Dispose()
        {
            _router.Unsubscribe(CANopenID.SDOTx(_nodeId));
            _responseEvent?.Dispose();
            _responseEvent = null;
        }

        private static byte[] ParseUploadResponse(byte[] data)
        {
            if (data == null || data.Length < 8) return new byte[0];
            // SCS = 2 for successful upload
            if (((data[0] & 0xE0) >> 5) != 2) return new byte[0];
            // Expedited: bit 1 (e) = 1 indicates expedited
            if ((data[0] & 0x02) != 0)
            {
                int n = (data[0] >> 2) & 0x03;
                int dlc = 4 - n;
                byte[] result = new byte[dlc];
                Array.Copy(data, 4, result, 0, dlc);
                return result;
            }
            // Fallback: return bytes 4-7
            byte[] raw = new byte[4];
            Array.Copy(data, 4, raw, 0, 4);
            return raw;
        }
    }
}
