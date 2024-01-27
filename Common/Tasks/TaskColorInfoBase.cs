using Common.MessageWriters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.Tasks
{
    public class TaskColorInfoBase<TId, TColor> : TaskInfoBase<TId>, ITaskColorInfo<TId, TColor>
        where TId : struct
        where TColor : struct
    {
        public TaskColorInfoBase(TId id, TColor textColor, int duration, int indentLevel,
            bool includeThreadId, IColorMessageWriter<TColor> messageWriter)
            : base(id, duration, indentLevel, includeThreadId, messageWriter)
        {
            TextColor = textColor;
            MessageWriter.DefaultTextColor = textColor;
        }

        public override IColorMessageWriter<TColor> MessageWriter
            => (IColorMessageWriter<TColor>)base.MessageWriter;

        public TColor TextColor { get; }

        public override void Write(string message)
        {
            MessageWriter.Write(message, TextColor, IndentLevel, IncludeThreadId);
        }

        public override void WriteWithElapsedTime(string message, DateTime startTime)
        {
            MessageWriter.WriteWithElapsedTime(message, startTime, TextColor, IndentLevel, IncludeThreadId);
        }
    }
}
