using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.MessageWriters
{
    public abstract class ColorMessageWriterBase<TColor> : MessageWriterBase, IColorMessageWriter<TColor>, IMessageWriter
        where TColor : struct
    {
        public ColorMessageWriterBase() { }

        public ColorMessageWriterBase(TColor defaultTextColor, bool includeThreadId)
            : base(includeThreadId)
        {
            DefaultTextColor = defaultTextColor;
        }

        public TColor DefaultTextColor { get; set; }

        public override void Write(string message, int indentLevel = 0, bool includeThreadId = false)
        {
            Write(message, DefaultTextColor, indentLevel, includeThreadId);
        }

        public abstract void Write(string message, TColor textColor,
            int indentLevel = 0, bool includeThreadId = false);

        public void WriteTimeTaken(string title, DateTime startTime, TColor textColor,
            int indentLevel = 0, bool includeThreadId = false)
        {
            string timeTakenText = GetTimeTakenText(startTime);
            string message = $"{title}: {timeTakenText}";

            Write(message, textColor, indentLevel, includeThreadId);
        }

        public void WriteTotalTimeTaken(DateTime startTime, TColor textColor,
            int indentLevel = 0, bool includeThreadId = false)
        {
            WriteTimeTaken("TOTAL TIME TAKEN", startTime, textColor, indentLevel, includeThreadId);
        }

        public void WriteWithElapsedTime(string message, DateTime startTime, TColor textColor,
            int indentLevel = 0, bool includeThreadId = false)
        {
            string timeTakenText = GetTimeTakenText(startTime);
            message = $"{timeTakenText} elapsed: {message}";

            Write(message, textColor, indentLevel, includeThreadId);
        }
    }
}
