using AsyncDEMO_Net8._2_AsyncVersions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDEMO_Net8._2_AsyncSimpleAwait
{
    internal class AsyncImmediateAwaitBreakfastMaker : AsyncBreakfastMakerBase
    {
        public override async Task MakeBreakfastAsync(DateTime startTime)
        {
            var textColor = ConsoleColor.DarkGray;

            ConsoleHelper.WriteInColor("Preparing breakfast:", textColor);
            Console.WriteLine();

            await HeatFryingPanAsync(startTime);
            Console.WriteLine();

            int numberOfEggs = 2;
            List<Egg> eggs = await FryEggsAsync(numberOfEggs, startTime);
            Console.WriteLine();

            int numberOfBaconSlices = 3;
            List<Bacon> baconSlices = await FryBaconAsync(numberOfBaconSlices, startTime);
            Console.WriteLine();

            Coffee cup = await MakeCoffeeAsync(startTime);
            Console.WriteLine();

            int numberOfToastSlices = 2;
            List<Toast> toastSlices = await MakeToastAsync(numberOfToastSlices, startTime);
            Console.WriteLine();

            SpreadButterOnToast(toastSlices, startTime);
            Console.WriteLine();

            SpreadJamOnToast(toastSlices, startTime);
            Console.WriteLine();

            Juice juice = PourJuice(startTime);
            Console.WriteLine();

            ConsoleHelper.WriteWithElapsedTime("Breakfast is ready!", startTime, textColor);
        }
    }
}
