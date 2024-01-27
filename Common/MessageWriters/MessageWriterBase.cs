using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.MessageWriters
{
    public abstract class MessageWriterBase : IMessageWriter
    {
        public MessageWriterBase() { }

        public MessageWriterBase(bool includeThreadId)
        {
            IncludeThreadId = includeThreadId;
        }

        public bool IncludeThreadId { get; set; }

        public void Write()
        {
            Write("");
        }

        public abstract void Write(string message, int indentLevel = 0, bool includeThreadId = false);

        public void WriteTimeTaken(string title, DateTime startTime, 
            int indentLevel = 0, bool includeThreadId = false)
        {
            string timeTakenText = GetTimeTakenText(startTime);
            string message = $"{title}: {timeTakenText}";

            Write(message, indentLevel, includeThreadId);
        }

        public void WriteTotalTimeTaken(DateTime startTime, 
            int indentLevel = 0, bool includeThreadId = false)
        {
            WriteTimeTaken("TOTAL TIME TAKEN", startTime, indentLevel, includeThreadId);
        }

        public void WriteWithElapsedTime(string message, DateTime startTime, 
            int indentLevel = 0, bool includeThreadId = false)
        {
            string timeTakenText = GetTimeTakenText(startTime);
            message = $"{timeTakenText} elapsed: {message}";

            Write(message, indentLevel, includeThreadId);
        }

        #region Protected methods *****************************************************************

        protected string GetTimeTakenText(DateTime startTime)
        {
            var currentTime = DateTime.Now;
            var timeTaken = currentTime - startTime;

            string message = $"{timeTaken.TotalSeconds.ToString(Constant.SecondsFormat)} seconds";

            return message;
        }

        protected string PrependThreadIdToMessage(string message)
        {
            if (message == null)
            {
                return message;
            }

            // Add a few smarts to handle the situation where an indent has already been 
            // added to the message - makes the method more flexible.
            string indentSpaces = ExtractLeadingSpace(message);
            message = message.TrimStart();

            int threadId = Environment.CurrentManagedThreadId;

            message = indentSpaces + $"Thread ID {threadId}: {message}";

            return message;
        }

        protected string AddIndentToMessage(string message, int indentLevel)
        {
            if (indentLevel <= 0)
            {
                return message;
            }

            // From Stackoverflow answer https://stackoverflow.com/a/3754630/216440
            message = string.Concat(Enumerable.Repeat(Constant.Indent, indentLevel)) + message;

            return message;
        }

        protected string ExtractLeadingSpace(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return message;
            }

            // Replaces everything in message, apart from leading spaces, with empty string.  
            // Leaves only the leading spaces.
            return message.Replace(message.TrimStart(), "");
        }

        #endregion Protected methods
    }
}
