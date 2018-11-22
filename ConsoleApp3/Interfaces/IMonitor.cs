namespace ConsoleApp3.Domain
{
    public interface IMonitor
    {
        void Write(string data);
        void Clear();
    }
}