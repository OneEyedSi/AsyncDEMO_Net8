using AsyncDEMO_Net8._1_SyncVersion;
using AsyncDEMO_Net8._2_AsyncSimpleAwait;
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
        [MenuMethod("Make breakfast synchronously")]
        public static void SyncBreakfast()
        {
            Console.WriteLine();
            Console.WriteLine("Making breakfast synchronously:");
            Console.WriteLine();

            WriteTaskDurations();

            var startTime = DateTime.Now;

            SyncBreakfastMaker.MakeBreakfast(startTime);
            Console.WriteLine();

            DoOtherWork(startTime);
            Console.WriteLine();

            ConsoleHelper.WriteTimeTaken(startTime);
        }

        [MenuMethod("Make breakfast asynchronously, with immediate await")]
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
            
            Console.WriteLine();

            DoOtherWork(startTime);

            await task;

            Console.WriteLine();

            ConsoleHelper.WriteTimeTaken(startTime);
        }

        private static void DoOtherWork(DateTime startTime)
        {
            var taskInfo = TaskInfo.GetTask(TaskId.OtherWork);

            taskInfo.WriteInTaskColor("Doing other work after making breakfast...");

            Task.Delay(taskInfo.Duration).Wait();

            taskInfo.WriteWithElapsedTime("Other work is finished", startTime);
        }

        private static void WriteTaskDurations()
        {
            Console.WriteLine("Individual task durations:");
            Console.WriteLine();

            Console.WriteLine("Breakfast tasks:");

            foreach (TaskInfo task in TaskInfo.GetTasks().Where(t => t.Id != TaskId.OtherWork))
            {
                Console.WriteLine(task.DurationText);
            }

            Console.WriteLine();

            TaskInfo otherWork = TaskInfo.GetTask(TaskId.OtherWork);
            Console.WriteLine(
                $"Other, non-breakfast, work: {otherWork.DurationSeconds.ToString(Constant.SecondsFormat)} seconds");

            Console.WriteLine();
        }
    }
}
