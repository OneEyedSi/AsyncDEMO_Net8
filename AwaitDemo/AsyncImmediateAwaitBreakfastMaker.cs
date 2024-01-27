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
    public class AsyncImmediateAwaitBreakfastMaker : AsyncBreakfastMakerBase
    {
        public AsyncImmediateAwaitBreakfastMaker(TaskListBase<AwaitDemoTaskId> taskList)
            : base(taskList) { }

        public override async Task MakeBreakfastAsync(DateTime startTime)
        {
            var taskInfo = _taskList.GetTask(AwaitDemoTaskId.MakeBreakfast);

            taskInfo.Write("Preparing breakfast:");

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

            taskInfo.WriteWithElapsedTime("Breakfast is ready!", startTime);
        }
    }
}
