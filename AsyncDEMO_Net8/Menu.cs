using AsyncDEMO_Net8._1_SyncVersion;
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

            var endTime = DateTime.Now;

            var timeTaken = endTime - startTime;

            Console.WriteLine($"Total time taken: {timeTaken.TotalSeconds.ToString(Constant.SecondsFormat)} seconds");
        }

            var endTime = DateTime.Now;

            var timeTaken = endTime - startTime;

            Console.WriteLine($"Total time taken: {timeTaken.TotalSeconds.ToString(Constant.SecondsFormat)} seconds");
        }

        private static void DoOtherWork(DateTime startTime)
        {
            var taskInfo = TaskInfo.GetTask(TaskId.OtherWork);

            taskInfo.WriteInTaskColor("Doing other work after making breakfast...");

            Task.Delay(Constant.TaskDuration.OtherWork).Wait();

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
