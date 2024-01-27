using Common.MessageWriters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.Tasks
{
    public class TaskInfoBase<TId> : ITaskInfo<TId> 
        where TId : struct
    {
        public TaskInfoBase(TId id, int duration, int indentLevel,
            bool includeThreadId, IMessageWriter messageWriter)
        {
            Id = id;
            Duration = duration;
            IndentLevel = indentLevel;
            IncludeThreadId = includeThreadId;

            MessageWriter = messageWriter;
            MessageWriter.IncludeThreadId = includeThreadId;
        }

        public TId Id { get; }

        public string Title => Regex.Replace(Id.ToString(), "(A-Z)", " $1");

        public int Duration { get; set; }

        public decimal DurationSeconds => decimal.Divide(Duration, 1000);

        public string DurationText =>
            $"{Title}: {DurationSeconds.ToString(Constant.SecondsFormat)} seconds";

        public int IndentLevel { get; set; }

        public bool IncludeThreadId { get; set; }

        public virtual IMessageWriter MessageWriter { get; }

        public IList<string> Tags { get; } = new List<string>();

        public void Write()
        {
            MessageWriter.Write();
        }

        public virtual void Write(string message)
        {
            MessageWriter.Write(message, IndentLevel, IncludeThreadId);
        }

        public virtual void WriteWithElapsedTime(string message, DateTime startTime)
        {
            MessageWriter.WriteWithElapsedTime(message, startTime, IndentLevel, IncludeThreadId);
        }
    }
}
