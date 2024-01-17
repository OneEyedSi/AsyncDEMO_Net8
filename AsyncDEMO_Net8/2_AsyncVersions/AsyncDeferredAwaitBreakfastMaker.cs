using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDEMO_Net8._2_AsyncVersions
{
    internal class AsyncDeferredAwaitBreakfastMaker : AsyncBreakfastMakerBase
    {
        public override async Task MakeBreakfastAsync(DateTime startTime)
        {
            var textColor = ConsoleColor.DarkGray;

            ConsoleHelper.WriteInColor("Preparing breakfast:", textColor);

            // Problem: Deferring the await for heating the pan and frying the eggs doesn't work: 
            // The pan must be heated before frying the eggs and the eggs must be finished before 
            // frying the bacon.
            Task heatPanTask = HeatFryingPanAsync(startTime);

            int numberOfEggs = 2;
            Task<List<Egg>> eggsTask = FryEggsAsync(numberOfEggs, startTime);

            int numberOfBaconSlices = 3;
            Task<List<Bacon>> baconSlicesTask = FryBaconAsync(numberOfBaconSlices, startTime);

            Task<Coffee> cupTask = MakeCoffeeAsync(startTime);

            int numberOfToastSlices = 2;
            Task<List<Toast>> toastSlicesTask = MakeToastAsync(numberOfToastSlices, startTime);

            await heatPanTask;
            var eggs = await eggsTask;
            var baconSlices = await baconSlicesTask;
            var cup = await cupTask;
            var toastSlices = await toastSlicesTask;

            // Second problem: Spreading butter and jam on the toast is delayed until the bacon
            // has finished frying and the coffee has been made, even though spreading butter and
            // jam is only dependent on the toast finishing.            
            SpreadButterOnToast(toastSlices, startTime);

            SpreadJamOnToast(toastSlices, startTime);

            Juice juice = PourJuice(startTime);

            ConsoleHelper.WriteWithElapsedTime("Breakfast is ready!", startTime, textColor);
        }
    }
}
