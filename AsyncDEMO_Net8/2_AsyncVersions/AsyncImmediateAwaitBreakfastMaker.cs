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

            await HeatFryingPanAsync(startTime);

            int numberOfEggs = 2;
            List<Egg> eggs = await FryEggsAsync(numberOfEggs, startTime);

            int numberOfBaconSlices = 3;
            List<Bacon> baconSlices = await FryBaconAsync(numberOfBaconSlices, startTime);

            Coffee cup = await MakeCoffeeAsync(startTime);

            int numberOfToastSlices = 2;
            List<Toast> toastSlices = await MakeToastAsync(numberOfToastSlices, startTime);

            SpreadButterOnToast(toastSlices, startTime);

            SpreadJamOnToast(toastSlices, startTime);

            Juice juice = PourJuice(startTime);

            ConsoleHelper.WriteWithElapsedTime("Breakfast is ready!", startTime, textColor);
        }
    }
}
