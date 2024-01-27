using AwaitDemo;
using AwaitDemo.Tasks;
using Common;
using Gold.ConsoleMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    [MenuClass("Await Menu", ParentMenuName = "Main Menu")]
    public class AwaitMenu
    {
        private static readonly AwaitDemoTaskList _taskList = new AwaitDemoTaskList();
        private static readonly ConsoleColor _defaultTextColor = ConsoleColor.Gray;

        [MenuMethod("List individual task durations", DisplayOrder = 1)]
        public static void DisplayTaskDurations()
        {
            var taskInfo = (AwaitDemoTaskInfo)_taskList.GetTask(AwaitDemoTaskId.ListTaskDurations);
            var messageWriter = taskInfo.MessageWriter;

            taskInfo.Write();
            taskInfo.Write("Individual task durations:");
            taskInfo.Write();

            string indent = Constant.Indent;

            taskInfo.Write($"{indent}Breakfast tasks:");
            decimal breakfastTotalTaskDuration = 0M;

            foreach (var task in _taskList.GetTasks().Where(t => t.Tags.Contains(TaskTag.BreakfastTask)))
            {
                taskInfo.Write(indent + indent + task.DurationText);
                breakfastTotalTaskDuration += task.DurationSeconds;
            }

            string message =
                $"Total duration of breakfast tasks: {breakfastTotalTaskDuration.ToString(Constant.SecondsFormat)} seconds";
            messageWriter.Write(indent + message, ConsoleColor.Yellow);

            var otherWorkTask = _taskList.GetTask(AwaitDemoTaskId.OtherWork);
            message =
                $"Other non-breakfast work performed by caller: {otherWorkTask.DurationText}";
            messageWriter.Write(indent + message, ConsoleColor.Yellow);

            string horizontalDivider = new('-', message.Length);

            decimal totalDuration = breakfastTotalTaskDuration + otherWorkTask.DurationSeconds;
            message = $"TOTAL DURATION OF ALL TASKS: {totalDuration.ToString(Constant.SecondsFormat)} seconds";
            taskInfo.Write(indent + horizontalDivider);
            taskInfo.Write(indent + message);

            taskInfo.Write();
            Console.ForegroundColor = _defaultTextColor;
        }

        [MenuMethod("Make breakfast synchronously", DisplayOrder = 2)]
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

        [MenuMethod("Make breakfast asynchronously, with immediate await", DisplayOrder = 3)]
        public static void AsyncBreakfast_ImmediateAwait()
        {
            var breakfastMaker = new AsyncImmediateAwaitBreakfastMaker(_taskList);
            var task = MakeBreakfastAsync("Making breakfast asynchronously, with immediate await:",
                breakfastMaker.MakeBreakfastAsync);
            // Note that this leaves all exceptions in async code wrapped in an AggregateException.
            // See Stackoverflow answer from Stephen Cleary: https://stackoverflow.com/a/9343733/216440
            task.Wait();
        }

        [MenuMethod("Make breakfast asynchronously, with deferred await", DisplayOrder = 4)]
        public static void AsyncBreakfast_DeferredAwait()
        {
            var breakfastMaker = new AsyncDeferredAwaitBreakfastMaker(_taskList);
            var task = MakeBreakfastAsync("Making breakfast asynchronously, with deferred await:",
                breakfastMaker.MakeBreakfastAsync);
            task.Wait();
        }

        [MenuMethod("Make breakfast asynchronously, with deferred await and composition", DisplayOrder = 5)]
        public static void AsyncBreakfast_WithComposition()
        {
            var breakfastMaker = new AsyncBreakfastMakerWithTaskComposition(_taskList);
            var task = MakeBreakfastAsync("Making breakfast asynchronously,\nwith deferred await and task composition:",
                breakfastMaker.MakeBreakfastAsync);
            task.Wait();
        }

        [MenuMethod("Make breakfast asynchronously, with mixed awaits and composition", DisplayOrder = 6)]
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
