using AwaitDemo;
using AwaitDemo.Tasks;
using Common;
using Gold.ConsoleMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreadingDemo;

namespace Main
{
    [MenuClass("Threading Menu", ParentMenuName = "Main Menu")]
    public class ThreadingMenu
    {
        private static readonly ThreadingDemoTaskList _taskList = new();
        private static readonly ConsoleColor _defaultTextColor = ConsoleColor.Gray;

        [MenuMethod("Make breakfast synchronously", DisplayOrder = 1)]
        public static void SyncBreakfast()
        {
            var taskInfo = (AwaitDemoTaskInfo)_taskList.GetTask(AwaitDemoTaskId.BreakfastCaller);
            var messageWriter = taskInfo.MessageWriter;

            taskInfo.Write();
            taskInfo.Write("Making breakfast synchronously:");
            taskInfo.Write();

            var startTime = DateTime.Now;

            var breakfastMaker = new SyncBreakfastMaker(_taskList);
            breakfastMaker.MakeBreakfast(startTime);

            DoOtherWork(startTime);
            taskInfo.Write();

            messageWriter.WriteTotalTimeTaken(startTime);
            Console.ForegroundColor = _defaultTextColor;
        }

        [MenuMethod("Make breakfast asynchronously, with immediate await", DisplayOrder = 2)]
        public static void AsyncBreakfast_ImmediateAwait()
        {
            var breakfastMaker = new AsyncImmediateAwaitBreakfastMaker(_taskList);
            var task = MakeBreakfastAsync("Making breakfast asynchronously, with immediate await:",
                breakfastMaker.MakeBreakfastAsync);
            // Note that this leaves all exceptions in async code wrapped in an AggregateException.
            // See Stackoverflow answer from Stephen Cleary: https://stackoverflow.com/a/9343733/216440
            task.Wait();
        }

        [MenuMethod("Make breakfast asynchronously, with deferred await", DisplayOrder = 3)]
        public static void AsyncBreakfast_DeferredAwait()
        {
            var breakfastMaker = new AsyncDeferredAwaitBreakfastMaker(_taskList);
            var task = MakeBreakfastAsync("Making breakfast asynchronously, with deferred await:",
                breakfastMaker.MakeBreakfastAsync);
            task.Wait();
        }

        [MenuMethod("Make breakfast asynchronously, with mixed awaits and composition", DisplayOrder = 4)]
        public static void AsyncBreakfast_MixedAwaits()
        {
            var breakfastMaker = new AsyncBreakfastMakerMixedAwaits(_taskList);
            var task = MakeBreakfastAsync("Making breakfast asynchronously,\nwith mixed awaits and task composition:",
                breakfastMaker.MakeBreakfastAsync);
            task.Wait();
        }

        private static async Task MakeBreakfastAsync(string title, Func<DateTime, Task> makeBreakfastMethodAsync)
        {
            var taskInfo = (AwaitDemoTaskInfo)_taskList.GetTask(AwaitDemoTaskId.BreakfastCaller);
            var messageWriter = taskInfo.MessageWriter;

            title = title.Trim();
            if (!title.EndsWith(':'))
            {
                title += ':';
            }

            taskInfo.Write();
            taskInfo.Write(title);
            taskInfo.Write();

            var startTime = DateTime.Now;

            var task = makeBreakfastMethodAsync(startTime);

            DoOtherWork(startTime);

            await task;

            taskInfo.Write();

            messageWriter.WriteTotalTimeTaken(startTime);
            Console.ForegroundColor = _defaultTextColor;
        }

        private static void DoOtherWork(DateTime startTime)
        {
            var taskInfo = (AwaitDemoTaskInfo)_taskList.GetTask(AwaitDemoTaskId.OtherWork);

            taskInfo.Write("Caller doing other non-breakfast work...");

            Task.Delay(taskInfo.Duration).Wait();

            taskInfo.WriteWithElapsedTime("Other work is finished", startTime);
        }
    }
}
