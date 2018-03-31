namespace Workshop.App.Interfaces
{
    internal interface IWriter
    {
        void Write(string line);

        void WriteLine(string line);

        void WriteLine(string format, params string[] args);
    }
}
