using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDEMO_Net8._1_SyncVersion
{
    public class SyncBreakfastMaker
    {
        public static void MakeBreakfast(DateTime startTime)
        {
            Coffee cup = MakeCoffee();
            TimestampedWrite("Coffee is ready", startTime);

            bool fryingPanIsHot = HeatFryingPan();
            TimestampedWrite("Frying pan is hot", startTime);

            int numberOfEggs = 2;
            List<Egg> eggs = FryEggs(numberOfEggs);
            TimestampedWrite("Eggs are ready", startTime);

            int numberOfBaconSlices = 3;
            List<Bacon> baconSlices = FryBacon(numberOfBaconSlices);
            TimestampedWrite("Bacon is ready", startTime);

            int numberOfToastSlices = 2;
            List<Toast> toastSlices = MakeToast(numberOfToastSlices);
            TimestampedWrite("Toast has popped", startTime);

            SpreadButterOnToast(toastSlices);
            SpreadJamOnToast(toastSlices);
            TimestampedWrite("Toast is ready", startTime);

            Juice juice = PourJuice();
            TimestampedWrite("Juice is ready", startTime);

            TimestampedWrite("Breakfast is ready!", startTime);
        }

        private static Coffee MakeCoffee()
        {
            Console.WriteLine("Making coffee...");

            Task.Delay(3000).Wait();

            return new Coffee();
        }

        private static bool HeatFryingPan()
        {
            Console.WriteLine("Heating frying pan...");

            bool isReady = false;

            Task.Delay(3000).Wait();

            isReady = true;

            return isReady;
        }

        private static List<Egg> FryEggs(int numberOfEggs)
        {
            Console.WriteLine($"Frying {numberOfEggs} eggs...");

            List<Egg> eggs = new();

            Task.Delay(3000).Wait();

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

            Task.Delay(3000).Wait();

            for (int i = 0; i < numberOfSlices; i++)
            {
                baconSlices.Add(new Bacon());
            }

            return baconSlices;
        }

        private static List<Toast> MakeToast(int numberOfSlices)
        {
            Console.WriteLine($"Toasting {numberOfSlices} slices of bread...");

            List<Toast> toastSlices = new();

            Task.Delay(3000).Wait();

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

        private static void TimestampedWrite (string text, DateTime startTime)
        {
            DateTime currentTime = DateTime.Now;
            var elapsedTime = currentTime - startTime;
            Console.WriteLine($"{elapsedTime.TotalSeconds.ToString(Constant.SecondsFormat)} seconds: {text}");
            Console.WriteLine();
        }
    }
}
