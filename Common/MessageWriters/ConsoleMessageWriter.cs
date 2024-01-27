using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.MessageWriters
{
    public class ConsoleMessageWriter : ColorMessageWriterBase<ConsoleColor>
    {
        public ConsoleMessageWriter(ConsoleColor defaultTextColor, bool includeThreadId)
            : base(defaultTextColor, includeThreadId) { }

        public override void Write(string message, ConsoleColor textColor,
            int indentLevel = 0, bool includeThreadId = false)
        {
            var previousConsoleColor = Console.ForegroundColor;
            Console.ForegroundColor = textColor;

            if (includeThreadId)
            {
                message = PrependThreadIdToMessage(message);
            }

            if (indentLevel > 0)
            {
                message = AddIndentToMessage(message, indentLevel);
            }

            Console.WriteLine(message);

            Console.ForegroundColor = previousConsoleColor;
        }
    }
}
