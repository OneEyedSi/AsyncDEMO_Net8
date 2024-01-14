using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AsyncDEMO_Net8
{
    internal enum TaskId
    {
        Unknown = 0,
        HeatPan,
        FryEggs,
        FryBacon,
        MakeCoffee,
        MakeToast,
        SpreadButter,
        SpreadJam,
        PourJuice,
        OtherWork
}

    internal class TaskInfo
    {
        private static readonly IList<TaskInfo> _tasks = InitializeTasks();
        public TaskInfo(TaskId id, ConsoleColor textColor, int duration)
        {
            Id = id;
            TextColor = textColor;
            Duration = duration;
        }

        public TaskId Id { get; set; }

        public string Title => Regex.Replace(Id.ToString(), "(A-Z)", " $1");

        public ConsoleColor TextColor { get; set; }

        public int Duration { get; set; }

        public decimal DurationSeconds => Decimal.Divide(Duration, 1000);

        public string DurationText => 
            $"{Title}: {DurationSeconds.ToString(Constant.SecondsFormat)} seconds";

        public void WriteInTaskColor(string message)
        {
            var previousConsoleColor = Console.ForegroundColor;

            Console.ForegroundColor = TextColor;

            string indent = new string(' ', 4);
            if (Id == TaskId.OtherWork || Id == TaskId.Unknown)
            {
                indent = "";
            }

            Console.WriteLine($"{indent}{message}");

            Console.ForegroundColor = previousConsoleColor;
        }

        public void WriteWithElapsedTime(string message, DateTime startTime)
        {
            DateTime currentTime = DateTime.Now;
            var elapsedTime = currentTime - startTime;
            message = $"{elapsedTime.TotalSeconds.ToString(Constant.SecondsFormat)} seconds elapsed: {message}";

            WriteInTaskColor(message);
        }

        public static IList<TaskInfo> GetTasks()
        {
            return _tasks;
        }

        public static TaskInfo GetTask(TaskId id) 
        { 
            if (id == TaskId.Unknown)
            {
                return new TaskInfo(TaskId.Unknown, ConsoleColor.White, 0);
            }

            return _tasks.First(t => t.Id == id);
        }

        private static List<TaskInfo> InitializeTasks()
        {
            List<TaskInfo> tasks = new();

            tasks.Add(new TaskInfo(TaskId.HeatPan, ConsoleColor.Magenta, 3000));
            tasks.Add(new TaskInfo(TaskId.FryEggs, ConsoleColor.DarkRed, 3000));
            tasks.Add(new TaskInfo(TaskId.FryBacon, ConsoleColor.Red, 5000));
            tasks.Add(new TaskInfo(TaskId.MakeCoffee, ConsoleColor.DarkYellow, 4000));
            tasks.Add(new TaskInfo(TaskId.MakeToast, ConsoleColor.Yellow, 2000));
            tasks.Add(new TaskInfo(TaskId.SpreadButter, ConsoleColor.Cyan, 0));
            tasks.Add(new TaskInfo(TaskId.SpreadJam, ConsoleColor.DarkCyan, 0));
            tasks.Add(new TaskInfo(TaskId.PourJuice, ConsoleColor.Blue, 0));
            tasks.Add(new TaskInfo(TaskId.OtherWork, ConsoleColor.DarkGreen, 5000));

            return tasks;
        }
    }
}
