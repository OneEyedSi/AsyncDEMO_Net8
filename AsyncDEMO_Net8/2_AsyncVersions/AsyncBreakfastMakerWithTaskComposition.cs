using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDEMO_Net8._2_AsyncVersions
{
    internal class AsyncBreakfastMakerWithTaskComposition : AsyncBreakfastMakerBase
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

            // The PrepareToastAsync method fixes the second problem: Now spreading butter and jam 
            // on the toast only has to wait for the toast to finish, it no longer has to wait 
            // until the bacon frying and the coffee making have finished.
            int numberOfToastSlices = 2;
            Task<List<Toast>> toastSlicesTask = PrepareToastAsync(numberOfToastSlices, startTime);

            await heatPanTask;
            var eggs = await eggsTask;
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
