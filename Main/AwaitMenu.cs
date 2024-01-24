using AwaitDemo;
using Common;
using Gold.ConsoleMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    [MenuClass("Await Menu", ParentMenuName = "Main Menu")]
    public class AwaitMenu
    {
        [MenuMethod("List individual task durations", DisplayOrder = 1)]
        public static void DisplayTaskDurations()
        {
            Console.WriteLine();
            Console.WriteLine("Individual task durations:");
            Console.WriteLine();

            string indent = Constant.Indent;

            Console.WriteLine($"{indent}Breakfast tasks:");
            decimal breakfastTotalTaskDuration = 0M;

            foreach (TaskInfo task in TaskInfo.GetTasks().Where(t => t.Id != TaskId.OtherWork))
            {
                Console.WriteLine(indent + indent + task.DurationText);
                breakfastTotalTaskDuration += task.DurationSeconds;
            }

            string message =
                $"Total duration of breakfast tasks: {breakfastTotalTaskDuration.ToString(Constant.SecondsFormat)} seconds";

            ConsoleHelper.WriteInColor(indent + message, ConsoleColor.Yellow);

            TaskInfo otherWork = TaskInfo.GetTask(TaskId.OtherWork);
            message =
                $"Other non-breakfast work performed by caller: {otherWork.DurationSeconds.ToString(Constant.SecondsFormat)} seconds";
            ConsoleHelper.WriteInColor(indent + message, ConsoleColor.Yellow);

            string horizontalDivider = new('-', message.Length);

            decimal totalDuration = breakfastTotalTaskDuration + otherWork.DurationSeconds;
            message = $"TOTAL DURATION OF ALL TASKS: {totalDuration.ToString(Constant.SecondsFormat)} seconds";
            Console.WriteLine(indent + horizontalDivider);
            Console.WriteLine(indent + message);

            Console.WriteLine();
        }

        [MenuMethod("Make breakfast synchronously", DisplayOrder = 2)]
        public static void SyncBreakfast()
        {
            Console.WriteLine();
            Console.WriteLine("Making breakfast synchronously:");
            Console.WriteLine();

            var startTime = DateTime.Now;

            SyncBreakfastMaker.MakeBreakfast(startTime);

            DoOtherWork(startTime);
            Console.WriteLine();

            ConsoleHelper.WriteTotalTimeTaken(startTime);
        }

        [MenuMethod("Make breakfast asynchronously, with immediate await", DisplayOrder = 3)]
        public static void AsyncBreakfast_ImmediateAwait()
        {
            var breakfastMaker = new AsyncImmediateAwaitBreakfastMaker();
            var task = MakeBreakfastAsync("Making breakfast asynchronously, with immediate await:",
                breakfastMaker.MakeBreakfastAsync);
            // Note that this leaves all exceptions in async code wrapped in an AggregateException.
            // See Stackoverflow answer from Stephen Cleary: https://stackoverflow.com/a/9343733/216440
            task.Wait();
        }

        [MenuMethod("Make breakfast asynchronously, with deferred await", DisplayOrder = 4)]
        public static void AsyncBreakfast_DeferredAwait()
        {
            var breakfastMaker = new AsyncDeferredAwaitBreakfastMaker();
            var task = MakeBreakfastAsync("Making breakfast asynchronously, with deferred await:",
                breakfastMaker.MakeBreakfastAsync);
            task.Wait();
        }

        [MenuMethod("Make breakfast asynchronously, with deferred await and composition", DisplayOrder = 5)]
        public static void AsyncBreakfast_WithComposition()
        {
            var breakfastMaker = new AsyncBreakfastMakerWithTaskComposition();
            var task = MakeBreakfastAsync("Making breakfast asynchronously,\nwith deferred await and task composition:",
                breakfastMaker.MakeBreakfastAsync);
            task.Wait();
        }

        [MenuMethod("Make breakfast asynchronously, with mixed awaits and composition", DisplayOrder = 6)]
        public static void AsyncBreakfast_MixedAwaits()
        {
            var breakfastMaker = new AsyncBreakfastMakerMixedAwaits();
            var task = MakeBreakfastAsync("Making breakfast asynchronously,\nwith mixed awaits and task composition:",
                breakfastMaker.MakeBreakfastAsync);
            task.Wait();
        }

        private static async Task MakeBreakfastAsync(string title, Func<DateTime, Task> makeBreakfastMethodAsync)
        {
            title = title.Trim();
            if (!title.EndsWith(':'))
            {
                title += ':';
            }

            Console.WriteLine();
            Console.WriteLine(title);
            Console.WriteLine();

            var startTime = DateTime.Now;

            var task = makeBreakfastMethodAsync(startTime);

            DoOtherWork(startTime);

            await task;

            Console.WriteLine();

            ConsoleHelper.WriteTotalTimeTaken(startTime);
        }

        private static void DoOtherWork(DateTime startTime)
        {
            var taskInfo = TaskInfo.GetTask(TaskId.OtherWork);

            taskInfo.WriteInTaskColor("Caller doing other non-breakfast work...");

            Task.Delay(taskInfo.Duration).Wait();

            taskInfo.WriteWithElapsedTime("Other work is finished", startTime);
        }
    }
}
