namespace ConsoleApp3.Domain
{
    internal interface IMonitor
    {
        void Write(string data);
        void Clear();
    }
}