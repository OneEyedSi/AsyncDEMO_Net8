using AwaitDemo.Tasks;
using Common;
using Common.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace AwaitDemo
{
    public class SyncBreakfastMaker
    {
        private readonly TaskListBase<AwaitDemoTaskId> _taskList;

        public SyncBreakfastMaker(TaskListBase<AwaitDemoTaskId> taskList)
        {
            _taskList = taskList;
        }

        public void MakeBreakfast(DateTime startTime)
        {
            var taskInfo = _taskList.GetTask(AwaitDemoTaskId.MakeBreakfast);

            taskInfo.Write("Preparing breakfast:");

            HeatFryingPan(startTime);

            int numberOfEggs = 2;
            List<Egg> eggs = FryEggs(numberOfEggs, startTime);

            int numberOfBaconSlices = 3;
            List<Bacon> baconSlices = FryBacon(numberOfBaconSlices, startTime);

            Coffee cup = MakeCoffee(startTime);

            int numberOfToastSlices = 2;
            List<Toast> toastSlices = MakeToast(numberOfToastSlices, startTime);

            SpreadButterOnToast(toastSlices, startTime);

            SpreadJamOnToast(toastSlices, startTime);

            Juice juice = PourJuice(startTime);

            taskInfo.WriteWithElapsedTime("Breakfast is ready!", startTime);
        }

        public void HeatFryingPan(DateTime startTime)
        {
            var taskInfo = _taskList.GetTask(AwaitDemoTaskId.HeatPan);

            taskInfo.Write("Heating frying pan...");

            Task.Delay(taskInfo.Duration).Wait();

            taskInfo.WriteWithElapsedTime("Frying pan is hot", startTime);
        }

        public List<Egg> FryEggs(int numberOfEggs, DateTime startTime)
        {
            var taskInfo = _taskList.GetTask(AwaitDemoTaskId.FryEggs);

            taskInfo.Write($"Frying {numberOfEggs} eggs...");

            List<Egg> eggs = new();

            Task.Delay(taskInfo.Duration).Wait();

            for (int i = 0; i < numberOfEggs; i++)
            {
                eggs.Add(new Egg());
            }

            taskInfo.WriteWithElapsedTime("Eggs are ready", startTime);

            return eggs;
        }

        public List<Bacon> FryBacon(int numberOfSlices, DateTime startTime)
        {
            var taskInfo = _taskList.GetTask(AwaitDemoTaskId.FryBacon);

            taskInfo.Write($"Frying {numberOfSlices} slices of bacon...");

            List<Bacon> baconSlices = new();

            Task.Delay(taskInfo.Duration).Wait();

            for (int i = 0; i < numberOfSlices; i++)
            {
                baconSlices.Add(new Bacon());
            }

            taskInfo.WriteWithElapsedTime("Bacon is ready", startTime);

            return baconSlices;
        }

        public Coffee MakeCoffee(DateTime startTime)
        {
            var taskInfo = _taskList.GetTask(AwaitDemoTaskId.MakeCoffee);

            taskInfo.Write("Making coffee...");

            Task.Delay(taskInfo.Duration).Wait();

            taskInfo.WriteWithElapsedTime("Coffee is ready", startTime);

            return new Coffee();
        }

        public List<Toast> MakeToast(int numberOfSlices, DateTime startTime)
        {
            var taskInfo = _taskList.GetTask(AwaitDemoTaskId.MakeToast);

            taskInfo.Write($"Toasting {numberOfSlices} slices of bread...");

            List<Toast> toastSlices = new();

            Task.Delay(taskInfo.Duration).Wait();

            for (int i = 0; i < numberOfSlices; i++)
            {
                toastSlices.Add(new Toast());
            }

            taskInfo.WriteWithElapsedTime("Toast has popped", startTime);

            return toastSlices;
        }

        public void SpreadButterOnToast(List<Toast> toastSlices, DateTime startTime)
        {
            var taskInfo = _taskList.GetTask(AwaitDemoTaskId.SpreadButter);

            taskInfo.Write($"Spreading butter on {toastSlices.Count} slices of toast...");

            Task.Delay(taskInfo.Duration).Wait();

            taskInfo.WriteWithElapsedTime("Butter has been spread", startTime);
        }

        public void SpreadJamOnToast(List<Toast> toastSlices, DateTime startTime)
        {
            var taskInfo = _taskList.GetTask(AwaitDemoTaskId.SpreadJam);

            taskInfo.Write($"Spreading jam on {toastSlices.Count} slices of toast...");

            Task.Delay(taskInfo.Duration).Wait();

            taskInfo.WriteWithElapsedTime("Jam has been spread", startTime);
        }

        public Juice PourJuice(DateTime startTime)
        {
            var taskInfo = _taskList.GetTask(AwaitDemoTaskId.PourJuice);

            taskInfo.Write("Pouring orange juice...");

            Task.Delay(taskInfo.Duration).Wait();

            taskInfo.WriteWithElapsedTime("Juice is ready", startTime);

            return new Juice();
        }
    }
}
