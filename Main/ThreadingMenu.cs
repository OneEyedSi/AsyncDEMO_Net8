using AwaitDemo;
using AwaitDemo.Tasks;
using Common;
using Common.MessageWriters;
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

        [MenuMethod("Simulate parallel threads", DisplayOrder = 5)]
        public static void SimulateParallelThreads()
        {
            var messageWriter = new ConsoleMessageWriter(ConsoleColor.Gray, includeThreadId: false);

            messageWriter.Write();
            messageWriter.Write("Simulating making breakfast on parallel threads:");
            messageWriter.Write();

            messageWriter.Write("Thread ID 1: Preparing breakfast:", ConsoleColor.DarkGray, indentLevel: 0);

            messageWriter.Write("Thread ID 2: Heating frying pan...", ConsoleColor.Magenta, indentLevel: 1);
            messageWriter.Write("Thread ID 3: Frying 2 eggs...", ConsoleColor.DarkRed, indentLevel: 1);
            messageWriter.Write("Thread ID 4: Heating 3 slices of bacon...", ConsoleColor.Red, indentLevel: 1);
            messageWriter.Write("Thread ID 5: Making coffee...", ConsoleColor.DarkYellow, indentLevel: 1);
            messageWriter.Write("Thread ID 6: Toasting 2 slices of bread...", ConsoleColor.Yellow, indentLevel: 1);

            messageWriter.Write("Thread ID 1: Caller doing other non-breakfast work...", ConsoleColor.DarkGreen, indentLevel: 0);

            messageWriter.Write("Thread ID 6: 2.1 seconds elapsed: Toast has popped", ConsoleColor.Yellow, indentLevel: 1);
            messageWriter.Write("Thread ID 2: 3.0 seconds elapsed: Frying pan is hot", ConsoleColor.Magenta, indentLevel: 1);
            messageWriter.Write("Thread ID 3: 3.0 seconds elapsed: Eggs are ready", ConsoleColor.DarkRed, indentLevel: 1);
            messageWriter.Write("Thread ID 5: 4.1 seconds elapsed: Coffee is ready", ConsoleColor.DarkYellow, indentLevel: 1);

            messageWriter.Write("Thread ID 1: 5.1 seconds elapsed: Other work is finished", ConsoleColor.DarkGreen, indentLevel: 0);

            messageWriter.Write("Thread ID 4: 5.1 seconds elapsed: Bacon is ready", ConsoleColor.Red, indentLevel: 1);
            messageWriter.Write("Thread ID 6: Spreading butter on 2 slices of toast...", ConsoleColor.Cyan, indentLevel: 1);
            messageWriter.Write("Thread ID 6: 6.1 seconds elapsed: Butter has been spread", ConsoleColor.Cyan, indentLevel: 1);
            messageWriter.Write("Thread ID 6: Spreading jam on 2 slices of toast...", ConsoleColor.DarkCyan, indentLevel: 1);
            messageWriter.Write("Thread ID 6: 7.1 seconds elapsed: Jam has been spread", ConsoleColor.DarkCyan, indentLevel: 1);
            messageWriter.Write("Thread ID 1: Pouring orange juice...", ConsoleColor.Blue, indentLevel: 1);
            messageWriter.Write("Thread ID 1: 7.6 seconds elapsed: Juice is ready", ConsoleColor.Blue, indentLevel: 1);

            messageWriter.Write("Thread ID 1: 7.6 seconds elapsed: Breakfast is ready!", ConsoleColor.DarkGray, indentLevel: 0);

            messageWriter.Write();
            messageWriter.Write("TOTAL TIME TAKEN: 7.6 seconds", ConsoleColor.Gray, indentLevel: 0);
        }
    }
}
