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
    public abstract class AsyncBreakfastMakerBase
    {
        protected readonly TaskListBase<AwaitDemoTaskId> _taskList;

        public AsyncBreakfastMakerBase(TaskListBase<AwaitDemoTaskId> taskList)
        {
            _taskList = taskList;
        }

        public abstract Task MakeBreakfastAsync(DateTime startTime);

        protected async Task HeatFryingPanAsync(DateTime startTime)
        {
            var taskInfo = _taskList.GetTask(AwaitDemoTaskId.HeatPan);

            taskInfo.Write("Heating frying pan...");

            await Task.Delay(taskInfo.Duration);

            taskInfo.WriteWithElapsedTime("Frying pan is hot", startTime);
        }

        protected async Task<List<Egg>> FryEggsAsync(int numberOfEggs, DateTime startTime)
        {
            var taskInfo = _taskList.GetTask(AwaitDemoTaskId.FryEggs);

            taskInfo.Write($"Frying {numberOfEggs} eggs...");

            List<Egg> eggs = new();

            await Task.Delay(taskInfo.Duration);

            for (int i = 0; i < numberOfEggs; i++)
            {
                eggs.Add(new Egg());
            }

            taskInfo.WriteWithElapsedTime("Eggs are ready", startTime);

            return eggs;
        }

        protected async Task<List<Bacon>> FryBaconAsync(int numberOfSlices, DateTime startTime)
        {
            var taskInfo = _taskList.GetTask(AwaitDemoTaskId.FryBacon);

            taskInfo.Write($"Frying {numberOfSlices} slices of bacon...");

            List<Bacon> baconSlices = new();

            await Task.Delay(taskInfo.Duration);

            for (int i = 0; i < numberOfSlices; i++)
            {
                baconSlices.Add(new Bacon());
            }

            taskInfo.WriteWithElapsedTime("Bacon is ready", startTime);

            return baconSlices;
        }

        protected async Task<Coffee> MakeCoffeeAsync(DateTime startTime)
        {
            var taskInfo = _taskList.GetTask(AwaitDemoTaskId.MakeCoffee);

            taskInfo.Write("Making coffee...");

            await Task.Delay(taskInfo.Duration);

            taskInfo.WriteWithElapsedTime("Coffee is ready", startTime);

            return new Coffee();
        }

        protected async Task<List<Toast>> MakeToastAsync(int numberOfSlices, DateTime startTime)
        {
            var taskInfo = _taskList.GetTask(AwaitDemoTaskId.MakeToast);

            taskInfo.Write($"Toasting {numberOfSlices} slices of bread...");

            List<Toast> toastSlices = new();

            await Task.Delay(taskInfo.Duration);

            for (int i = 0; i < numberOfSlices; i++)
            {
                toastSlices.Add(new Toast());
            }

            taskInfo.WriteWithElapsedTime("Toast has popped", startTime);

            return toastSlices;
        }

        protected void SpreadButterOnToast(List<Toast> toastSlices, DateTime startTime)
        {
            var taskInfo = _taskList.GetTask(AwaitDemoTaskId.SpreadButter);

            taskInfo.Write($"Spreading butter on {toastSlices.Count} slices of toast...");

            Task.Delay(taskInfo.Duration).Wait();

            taskInfo.WriteWithElapsedTime("Butter has been spread", startTime);
        }

        protected void SpreadJamOnToast(List<Toast> toastSlices, DateTime startTime)
        {
            var taskInfo = _taskList.GetTask(AwaitDemoTaskId.SpreadJam);

            taskInfo.Write($"Spreading jam on {toastSlices.Count} slices of toast...");

            Task.Delay(taskInfo.Duration).Wait();

            taskInfo.WriteWithElapsedTime("Jam has been spread", startTime);
        }

        protected Juice PourJuice(DateTime startTime)
        {
            var taskInfo = _taskList.GetTask(AwaitDemoTaskId.PourJuice);

            taskInfo.Write("Pouring orange juice...");

            Task.Delay(taskInfo.Duration).Wait();

            taskInfo.WriteWithElapsedTime("Juice is ready", startTime);

            return new Juice();
        }
    }
}
