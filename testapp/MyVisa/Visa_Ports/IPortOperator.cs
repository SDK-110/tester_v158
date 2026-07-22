namespace MyVISAInstrument.Ports
{
    internal interface IPortOperator
    {
        void Open();
        void Close();
        void Write(string command);
        string Read();
    }
}