using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDEMO_Net8._1_SyncVersion
{
    internal class SyncBreakfastMaker
    {
        public static void MakeBreakfast(DateTime startTime)
        {
            var textColor = ConsoleColor.DarkGray;

            ConsoleHelper.WriteInColor("Preparing breakfast:", textColor);
            Console.WriteLine();

            HeatFryingPan(startTime);
            Console.WriteLine();

            int numberOfEggs = 2;
            List<Egg> eggs = FryEggs(numberOfEggs, startTime);
            Console.WriteLine();

            int numberOfBaconSlices = 3;
            List<Bacon> baconSlices = FryBacon(numberOfBaconSlices, startTime);
            Console.WriteLine();

            Coffee cup = MakeCoffee(startTime);
            Console.WriteLine();

            int numberOfToastSlices = 2;
            List<Toast> toastSlices = MakeToast(numberOfToastSlices, startTime);
            Console.WriteLine();

            SpreadButterOnToast(toastSlices, startTime);
            Console.WriteLine();

            SpreadJamOnToast(toastSlices, startTime);
            Console.WriteLine();

            Juice juice = PourJuice(startTime);
            Console.WriteLine();

            ConsoleHelper.WriteWithElapsedTime("Breakfast is ready!", startTime, textColor);
        }

        private static void HeatFryingPan(DateTime startTime)
        {
            var taskInfo = TaskInfo.GetTask(TaskId.HeatPan);

            taskInfo.WriteInTaskColor("Heating frying pan...");

            Task.Delay(taskInfo.Duration).Wait();

            taskInfo.WriteWithElapsedTime("Frying pan is hot", startTime);
        }

        private static List<Egg> FryEggs(int numberOfEggs, DateTime startTime)
        {
            var taskInfo = TaskInfo.GetTask(TaskId.FryEggs);

            taskInfo.WriteInTaskColor($"Frying {numberOfEggs} eggs...");

            List<Egg> eggs = new();

            Task.Delay(taskInfo.Duration).Wait();

            for (int i = 0; i < numberOfEggs; i++)
            {
                eggs.Add(new Egg());
            }

            taskInfo.WriteWithElapsedTime("Eggs are ready", startTime);

            return eggs;
        }

        private static List<Bacon> FryBacon(int numberOfSlices, DateTime startTime)
        {
            var taskInfo = TaskInfo.GetTask(TaskId.FryBacon);

            taskInfo.WriteInTaskColor($"Frying {numberOfSlices} slices of bacon...");

            List<Bacon> baconSlices = new();

            Task.Delay(taskInfo.Duration).Wait();

            for (int i = 0; i < numberOfSlices; i++)
            {
                baconSlices.Add(new Bacon());
            }

            taskInfo.WriteWithElapsedTime("Bacon is ready", startTime);

            return baconSlices;
        }

        private static Coffee MakeCoffee(DateTime startTime)
        {
            var taskInfo = TaskInfo.GetTask(TaskId.MakeCoffee);

            taskInfo.WriteInTaskColor("Making coffee...");

            Task.Delay(taskInfo.Duration).Wait();

            taskInfo.WriteWithElapsedTime("Coffee is ready", startTime);

            return new Coffee();
        }

        private static List<Toast> MakeToast(int numberOfSlices, DateTime startTime)
        {
            var taskInfo = TaskInfo.GetTask(TaskId.MakeToast);

            taskInfo.WriteInTaskColor($"Toasting {numberOfSlices} slices of bread...");

            List<Toast> toastSlices = new();

            Task.Delay(taskInfo.Duration).Wait();

            for (int i = 0; i < numberOfSlices; i++)
            {
                toastSlices.Add(new Toast());
            }

            taskInfo.WriteWithElapsedTime("Toast has popped", startTime);

            return toastSlices;
        }

        private static void SpreadButterOnToast(List<Toast> toastSlices, DateTime startTime)
        {
            var taskInfo = TaskInfo.GetTask(TaskId.SpreadButter);

            taskInfo.WriteInTaskColor($"Spreading butter on {toastSlices.Count} slices of toast...");

            taskInfo.WriteWithElapsedTime("Butter has been spread", startTime);
        }

        private static void SpreadJamOnToast(List<Toast> toastSlices, DateTime startTime)
        {
            var taskInfo = TaskInfo.GetTask(TaskId.SpreadJam);

            taskInfo.WriteInTaskColor($"Spreading jam on {toastSlices.Count} slices of toast...");

            taskInfo.WriteWithElapsedTime("Jam has been spread", startTime);
        }

        private static Juice PourJuice(DateTime startTime)
        {
            var taskInfo = TaskInfo.GetTask(TaskId.PourJuice);

            taskInfo.WriteInTaskColor("Pouring orange juice...");

            taskInfo.WriteWithElapsedTime("Juice is ready", startTime);

            return new Juice();
        }
    }
}
