
namespace Common.MessageWriters
{
    public interface IMessageWriter
    {
        bool IncludeThreadId { get; set; }

        void Write();
        void Write(string message, int indentLevel = 0, bool includeThreadId = false);
        void WriteTimeTaken(string title, DateTime startTime, int indentLevel = 0, bool includeThreadId = false);
        void WriteTotalTimeTaken(DateTime startTime, int indentLevel = 0, bool includeThreadId = false);
        void WriteWithElapsedTime(string message, DateTime startTime, int indentLevel = 0, bool includeThreadId = false);
    }
}