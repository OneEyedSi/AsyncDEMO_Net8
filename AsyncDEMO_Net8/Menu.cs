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
            var startTime = DateTime.Now;

            Console.WriteLine("Making breakfast synchronously:");
            Console.WriteLine();

            WriteTaskDurations();

            SyncBreakfastMaker.MakeBreakfast(startTime);

            DoOtherWork(startTime);

            var endTime = DateTime.Now;

            var timeTaken = endTime - startTime;

            Console.WriteLine($"Total time taken: {timeTaken.TotalSeconds.ToString(Constant.SecondsFormat)} seconds");
        }

        private static void DoOtherWork(DateTime startTime)
        {
            var previousConsoleColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("Doing other work after making breakfast...");

            Task.Delay(Constant.TaskDuration.OtherWork).Wait();

            Helper.WriteWithElapsedTime("Other work is finished", startTime);

            Console.ForegroundColor = previousConsoleColor;
        }

        private static void WriteTaskDurations()
        {
            Console.WriteLine("Individual task durations:");
            Console.WriteLine();

            Console.WriteLine("Breakfast tasks:");
            WriteTaskDuration("Heat pan", Constant.TaskDuration.HeatPan);
            WriteTaskDuration("Fry eggs", Constant.TaskDuration.FryEggs);
            WriteTaskDuration("Fry bacon", Constant.TaskDuration.FryBacon);
            WriteTaskDuration("Make coffee", Constant.TaskDuration.MakeCoffee);
            WriteTaskDuration("Make toast", Constant.TaskDuration.MakeToast);
            WriteTaskDuration("Add butter and jam to toast", 0);
            WriteTaskDuration("Pour juice", 0);
            Console.WriteLine();

            WriteTaskDuration("Other, non-breakfast, work", Constant.TaskDuration.OtherWork);

            Console.WriteLine();
        }

        private static void WriteTaskDuration(string taskTitle, int durationMilliseconds)
        {
            string durationSecondsText = Decimal.Divide(durationMilliseconds, 1000).ToString(Constant.SecondsFormat);
            Console.WriteLine($"{taskTitle}: {durationSecondsText} seconds");
        }
    }
}
