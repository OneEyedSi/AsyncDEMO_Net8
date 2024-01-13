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
            var previousConsoleColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("Preparing breakfast:");
            Console.WriteLine();

            bool fryingPanIsHot = HeatFryingPan();
            Helper.WriteWithElapsedTime("Frying pan is hot", startTime);

            int numberOfEggs = 2;
            List<Egg> eggs = FryEggs(numberOfEggs);
            Helper.WriteWithElapsedTime("Eggs are ready", startTime);

            int numberOfBaconSlices = 3;
            List<Bacon> baconSlices = FryBacon(numberOfBaconSlices);
            Helper.WriteWithElapsedTime("Bacon is ready", startTime);

            Coffee cup = MakeCoffee();
            Helper.WriteWithElapsedTime("Coffee is ready", startTime);

            int numberOfToastSlices = 2;
            List<Toast> toastSlices = MakeToast(numberOfToastSlices);
            Helper.WriteWithElapsedTime("Toast has popped", startTime);

            SpreadButterOnToast(toastSlices);
            SpreadJamOnToast(toastSlices);
            Helper.WriteWithElapsedTime("Toast is ready", startTime);

            Juice juice = PourJuice();
            Helper.WriteWithElapsedTime("Juice is ready", startTime);

            Helper.WriteWithElapsedTime("Breakfast is ready!", startTime);

            Console.ForegroundColor = previousConsoleColor;
        }

        private static bool HeatFryingPan()
        {
            Console.WriteLine("Heating frying pan...");

            bool isReady = false;

            Task.Delay(Constant.TaskDuration.HeatPan).Wait();

            isReady = true;

            return isReady;
        }

        private static List<Egg> FryEggs(int numberOfEggs)
        {
            Console.WriteLine($"Frying {numberOfEggs} eggs...");

            List<Egg> eggs = new();

            Task.Delay(Constant.TaskDuration.FryEggs).Wait();

            for(int i = 0; i < numberOfEggs; i++)
            {
                eggs.Add(new Egg());
            }

            return eggs;
        }

        private static List<Bacon> FryBacon(int numberOfSlices)
        {
            Console.WriteLine($"Frying {numberOfSlices} slices of bacon...");

            List<Bacon> baconSlices = new();

            Task.Delay(Constant.TaskDuration.FryBacon).Wait();

            for (int i = 0; i < numberOfSlices; i++)
            {
                baconSlices.Add(new Bacon());
            }

            return baconSlices;
        }

        private static Coffee MakeCoffee()
        {
            Console.WriteLine("Making coffee...");

            Task.Delay(Constant.TaskDuration.MakeCoffee).Wait();

            return new Coffee();
        }

        private static List<Toast> MakeToast(int numberOfSlices)
        {
            Console.WriteLine($"Toasting {numberOfSlices} slices of bread...");

            List<Toast> toastSlices = new();

            Task.Delay(Constant.TaskDuration.MakeToast).Wait();

            for (int i = 0; i < numberOfSlices; i++)
            {
                toastSlices.Add(new Toast());
            }

            return toastSlices;
        }

        private static void SpreadButterOnToast(List<Toast> toastSlices)
        {
            Console.WriteLine($"Spreading butter on {toastSlices.Count} slices of toast...");
        }

        private static void SpreadJamOnToast(List<Toast> toastSlices)
        {
            Console.WriteLine($"Spreading jam on {toastSlices.Count} slices of toast...");
        }

        private static Juice PourJuice()
        {
            Console.WriteLine("Pouring orange juice...");

            return new Juice();
        }
    }
}
