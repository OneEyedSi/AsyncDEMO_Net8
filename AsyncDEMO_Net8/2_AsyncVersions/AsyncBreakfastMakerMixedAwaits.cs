using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDEMO_Net8._2_AsyncVersions
{
    internal class AsyncBreakfastMakerMixedAwaits : AsyncBreakfastMakerBase
    {
        public override async Task MakeBreakfastAsync(DateTime startTime)
        {
            var textColor = ConsoleColor.DarkGray;

            ConsoleHelper.WriteInColor("Preparing breakfast:", textColor);

            // Use immediate awaits for heating the pan and frying the eggs: 
            // This will ensure the pan finishes heating before starting to fry the eggs, and the 
            // eggs finish frying before starting to fry the bacon.  
            await HeatFryingPanAsync(startTime);

            int numberOfEggs = 2;
            List<Egg> eggs = await FryEggsAsync(numberOfEggs, startTime);

            // Defer the await for frying the bacon: We're happy for it to run in parallel with 
            // making the coffee and preparing the toast.  Running the three tasks in parallel 
            // reduces execution time.
            int numberOfBaconSlices = 3;
            Task<List<Bacon>> baconSlicesTask = FryBaconAsync(numberOfBaconSlices, startTime);

            Task<Coffee> cupTask = MakeCoffeeAsync(startTime);

            // The PrepareToastAsync method fixes the second problem: Now spreading butter and jam 
            // on the toast only has to wait for the toast to finish, it no longer has to wait 
            // until the bacon frying and the coffee making have finished.
            int numberOfToastSlices = 2;
            Task<List<Toast>> toastSlicesTask = PrepareToastAsync(numberOfToastSlices, startTime);

            var baconSlices = await baconSlicesTask;
            var cup = await cupTask;
            var toastSlices = await toastSlicesTask;

            Juice juice = PourJuice(startTime);

            ConsoleHelper.WriteWithElapsedTime("Breakfast is ready!", startTime, textColor);
        }

        private async Task<List<Toast>> PrepareToastAsync(int numberOfSlices, DateTime startTime)
        {
            List<Toast> toastSlices = await MakeToastAsync(numberOfSlices, startTime);

            SpreadButterOnToast(toastSlices, startTime);

            SpreadJamOnToast(toastSlices, startTime);

            return toastSlices;
        }
    }
}
