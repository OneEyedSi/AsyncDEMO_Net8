using Common;
using Common.Tasks;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AwaitDemo.Tasks
{
    public class AwaitDemoTaskList : TaskListBase<AwaitDemoTaskId>
    {
        public AwaitDemoTaskList() : base() { }

        protected override IList<ITaskInfo<AwaitDemoTaskId>> InitializeTasks()
        {
            var tasks = new List<AwaitDemoTaskInfo>
            {
                new(AwaitDemoTaskId.HeatPan, ConsoleColor.Magenta, 3000, 1, false),
                new(AwaitDemoTaskId.FryEggs, ConsoleColor.DarkRed, 3000, 1, false),
                new(AwaitDemoTaskId.FryBacon, ConsoleColor.Red, 5000, 1, false),
                new(AwaitDemoTaskId.MakeCoffee, ConsoleColor.DarkYellow, 4000, 1, false),
                new(AwaitDemoTaskId.MakeToast, ConsoleColor.Yellow, 2000, 1, false),
                new(AwaitDemoTaskId.SpreadButter, ConsoleColor.Cyan, 1000, 1, false),
                new(AwaitDemoTaskId.SpreadJam, ConsoleColor.DarkCyan, 1000, 1, false),
                new(AwaitDemoTaskId.PourJuice, ConsoleColor.Blue, 500, 1, false),
                new(AwaitDemoTaskId.MakeBreakfast, ConsoleColor.DarkGray, 0, 0, false),
                new(AwaitDemoTaskId.OtherWork, ConsoleColor.DarkGreen, 5000, 0, false),
                new(AwaitDemoTaskId.BreakfastCaller, ConsoleColor.Gray, 0, 0, false),
                new(AwaitDemoTaskId.ListTaskDurations, ConsoleColor.Gray, 0, 0, false)
            };

            var breakfastTaskIds = new AwaitDemoTaskId[] { AwaitDemoTaskId.HeatPan,
                                                            AwaitDemoTaskId.FryEggs,
                                                            AwaitDemoTaskId.FryBacon,
                                                            AwaitDemoTaskId.MakeCoffee,
                                                            AwaitDemoTaskId.MakeToast,
                                                            AwaitDemoTaskId.SpreadButter,
                                                            AwaitDemoTaskId.SpreadJam,
                                                            AwaitDemoTaskId.PourJuice
                                                        };

            foreach (var task in tasks)
            {
                if (breakfastTaskIds.Contains(task.Id))
                {
                    task.Tags.Add(TaskTag.BreakfastTask);
                }
            }

            return tasks.Cast<ITaskInfo<AwaitDemoTaskId>>().ToList();
        }
    }
}
