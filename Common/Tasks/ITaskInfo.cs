
using Common.MessageWriters;

namespace Common.Tasks
{
    public interface ITaskInfo<TId> where TId : struct
    {
        TId Id { get; }
        string Title { get; }
        int Duration { get; set; }
        decimal DurationSeconds { get; }
        string DurationText { get; }
        int IndentLevel { get; set; }
        bool IncludeThreadId { get; set; }
        IMessageWriter MessageWriter { get; }
        IList<string> Tags { get; }

        void Write();
        void Write(string message);
        void WriteWithElapsedTime(string message, DateTime startTime);
    }
}