using AsyncDEMO_Net8._1_SyncVersion;
using AsyncDEMO_Net8._2_AsyncSimpleAwait;
using AsyncDEMO_Net8._2_AsyncVersions;
using Gold.ConsoleMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDEMO_Net8
{
    [MenuClass("Menu")]
    public class Menu
    {
        [MenuMethod("Make breakfast synchronously", DisplayOrder = 1)]
        public static void SyncBreakfast()
        {
            Console.WriteLine();
            Console.WriteLine("Making breakfast synchronously:");
            Console.WriteLine();

            WriteTaskDurations();

            var startTime = DateTime.Now;

            SyncBreakfastMaker.MakeBreakfast(startTime);

            DoOtherWork(startTime);
            Console.WriteLine();

            ConsoleHelper.WriteTotalTimeTaken(startTime);
        }

        [MenuMethod("Make breakfast asynchronously, with immediate await", DisplayOrder = 2)]
        public static void AsyncBreakfast_ImmediateAwait()
        {
            var breakfastMaker = new AsyncImmediateAwaitBreakfastMaker();
            var task = MakeBreakfastAsync("Making breakfast asynchronously, with immediate await:",
                breakfastMaker.MakeBreakfastAsync);
            // Note that this leaves all exceptions in async code wrapped in an AggregateException.
            // See Stackoverflow answer from Stephen Cleary: https://stackoverflow.com/a/9343733/216440
            task.Wait();
        }

        private static async Task MakeBreakfastAsync(string title, Func<DateTime, Task> makeBreakfastMethodAync)
        {
            title = title.Trim();
            if (!title.EndsWith(':'))
            {
                title += ':';
            }

            Console.WriteLine();
            Console.WriteLine(title);
            Console.WriteLine();

            WriteTaskDurations();

            var startTime = DateTime.Now;

            var task = makeBreakfastMethodAync(startTime);

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

        private static void WriteTaskDurations()
        {
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

            Console.WriteLine(
                $"{indent}Total duration of breakfast tasks: {breakfastTotalTaskDuration} seconds");

            TaskInfo otherWork = TaskInfo.GetTask(TaskId.OtherWork);
            Console.WriteLine(
                $"{indent}Other non-breakfast work: {otherWork.DurationSeconds.ToString(Constant.SecondsFormat)} seconds");

            decimal totalDuration = breakfastTotalTaskDuration + otherWork.DurationSeconds;
            string message = $"TOTAL DURATION OF ALL TASKS: {totalDuration} seconds";
            string horizontalDivider = new string('-', message.Length);
            Console.WriteLine(indent + horizontalDivider);
            Console.WriteLine(indent + message);

            Console.WriteLine();
        }
    }
}
