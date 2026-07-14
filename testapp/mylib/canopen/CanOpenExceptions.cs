using System;

namespace testapp.mylib.canopen
{
    public class CanOpenException : Exception
    {
        public CanOpenException(string message) : base(message) { }
        public CanOpenException(string message, Exception inner) : base(message, inner) { }
    }

    public class NodeNotRegisteredException : CanOpenException
    {
        public byte NodeId { get; }
        public NodeNotRegisteredException(byte nodeId)
            : base($"Node {nodeId} is not registered") { NodeId = nodeId; }
    }

    public class TransportException : CanOpenException
    {
        public TransportException(string message, Exception inner = null)
            : base(message, inner) { }
    }
}
