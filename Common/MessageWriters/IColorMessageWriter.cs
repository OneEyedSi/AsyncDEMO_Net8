
namespace Common.MessageWriters
{
    public interface IColorMessageWriter<TColor> : IMessageWriter 
        where TColor : struct
    {
        TColor DefaultTextColor { get; set; }

        void Write(string message, TColor textColor, int indentLevel = 0, bool includeThreadId = false);
        void WriteTimeTaken(string title, DateTime startTime, TColor textColor, int indentLevel = 0, bool includeThreadId = false);
        void WriteTotalTimeTaken(DateTime startTime, TColor textColor, int indentLevel = 0, bool includeThreadId = false);
        void WriteWithElapsedTime(string message, DateTime startTime, TColor textColor, int indentLevel = 0, bool includeThreadId = false);
    }
}