using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AsyncDEMO_Net8
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

        public static void WriteInColor(string message, ConsoleColor textColor)
        {
            var previousConsoleColor = Console.ForegroundColor;
            Console.ForegroundColor = textColor;
            Console.WriteLine(message);

            Console.ForegroundColor = previousConsoleColor;
        }

        public static void WriteTimeTaken(DateTime startTime)
        {
            var endTime = DateTime.Now;

            var timeTaken = endTime - startTime;

            Console.WriteLine($"Total time taken: {timeTaken.TotalSeconds.ToString(Constant.SecondsFormat)} seconds");
        }
    }
}
