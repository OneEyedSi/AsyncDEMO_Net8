using AwaitDemo.Tasks;
using Common;
using Common.Tasks;
using System.Threading.Tasks;

namespace ThreadingDemo
{
    public class ThreadingDemoTaskList : AwaitDemoTaskList
    {
        protected override IList<ITaskInfo<AwaitDemoTaskId>> InitializeTasks()
        {
            var tasks = base.InitializeTasks();

            foreach (var task in tasks.Where(t => t.Tags.Contains(TaskTag.BreakfastTask)
                                                || t.Id == AwaitDemoTaskId.MakeBreakfast
                                                || t.Id == AwaitDemoTaskId.OtherWork))
            {
                task.IncludeThreadId = true;
            }

            return tasks;
        }
    }
}
