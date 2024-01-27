using AwaitDemo.Tasks;
using Common;
using Common.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwaitDemo
{
    public class AsyncBreakfastMakerWithTaskComposition : AsyncBreakfastMakerBase
    {
        public AsyncBreakfastMakerWithTaskComposition(TaskListBase<AwaitDemoTaskId> taskList)
            : base(taskList) { }

        public override async Task MakeBreakfastAsync(DateTime startTime)
        {
            var taskInfo = _taskList.GetTask(AwaitDemoTaskId.MakeBreakfast);

            taskInfo.Write("Preparing breakfast:");

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

            taskInfo.WriteWithElapsedTime("Breakfast is ready!", startTime);
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
