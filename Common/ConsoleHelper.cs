using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Common
{
    public class ConsoleHelper
    {
        public ConsoleHelper() : this(ConsoleColor.White) { }

        public ConsoleHelper(ConsoleColor defaultConsoleTextColor)
        {
            DefaultConsoleTextColor = defaultConsoleTextColor;
        }

        public ConsoleColor DefaultConsoleTextColor { get; set; }

        public void WriteWithElapsedTime(string message, DateTime startTime)
        {
            WriteWithElapsedTime(message, startTime, DefaultConsoleTextColor);
        }

        public void WriteInColor(string message)
        {
            WriteInColor(message, DefaultConsoleTextColor);
        }

        public static void WriteWithElapsedTime(string message, DateTime startTime, ConsoleColor textColor)
        {
            DateTime currentTime = DateTime.Now;
            var elapsedTime = currentTime - startTime;
            message = $"{elapsedTime.TotalSeconds.ToString(Constant.SecondsFormat)} seconds elapsed: {message}";

            WriteInColor(message, textColor);
        }

        public static void WriteInColor(string message, ConsoleColor textColor, int indentLevel = 0)
        {
            var previousConsoleColor = Console.ForegroundColor;
            Console.ForegroundColor = textColor;

            int threadId = System.Environment.CurrentManagedThreadId;

            message = PrependThreadIdToMessage(message);
            message = AddIndentToMessage(message, indentLevel);

            Console.WriteLine(message);

            Console.ForegroundColor = previousConsoleColor;
        }

        public static void WriteTimeTaken(string title, DateTime startTime, int indentLevel = 0)
        {
            string message = GetTimeTakenMessage(title, startTime, indentLevel);
            message = PrependThreadIdToMessage(message);
            // Will write in whatever the previously set console color is.
            Console.WriteLine(message);
        }

        public static void WriteTimeTaken(string title, DateTime startTime, ConsoleColor textColor, 
            int indentLevel = 0)
        {
            string message = GetTimeTakenMessage(title, startTime, indentLevel);
            WriteInColor(message, textColor);
        }

        public static void WriteTotalTimeTaken(DateTime startTime)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            WriteTimeTaken("TOTAL TIME TAKEN", startTime);
        }

        private static string GetTimeTakenMessage(string title, DateTime startTime, int indentLevel)
        {
            var endTime = DateTime.Now;

            var timeTaken = endTime - startTime;

            string message = $"{title}: {timeTaken.TotalSeconds.ToString(Constant.SecondsFormat)} seconds";
            message = AddIndentToMessage(message, indentLevel);

            return message;
        }

        private static string PrependThreadIdToMessage(string message)
        {
            if (message == null)
            {
                return message;
            }

            // Add a few smarts to handle the situation where an indent has already been 
            // added to the message - makes the method more flexible.
            string indentSpaces = ExtractLeadingSpace(message);
            message = message.TrimStart();

            int threadId = System.Environment.CurrentManagedThreadId;

            message = indentSpaces + $"Thread ID {threadId}: {message}";

            return message;
        }

        private static string AddIndentToMessage(string message, int indentLevel)
        {
            if (indentLevel <= 0)
            {
                return message;
            }

            // From Stackoverflow answer https://stackoverflow.com/a/3754630/216440
            message = string.Concat(Enumerable.Repeat(Constant.Indent, indentLevel)) + message;

            return message;
        }

        private static string ExtractLeadingSpace(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return message;
            }

            // Replaces everything in message, apart from leading spaces, with empty string.  
            // Leaves only the leading spaces.
            return message.Replace(message.TrimStart(), "");
        }
    }
}
