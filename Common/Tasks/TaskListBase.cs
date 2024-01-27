using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Tasks
{
    public abstract class TaskListBase<TId> where TId : struct
    {
        private readonly IList<ITaskInfo<TId>> _tasks;

        public TaskListBase()
        {
            _tasks = InitializeTasks();
        }

        public IList<ITaskInfo<TId>> GetTasks()
        {
            return _tasks;
        }

        public ITaskInfo<TId> GetTask(TId id)
        {
            return _tasks.First(t => EqualityComparer<TId>.Default.Equals(t.Id, id));
        }

        protected abstract IList<ITaskInfo<TId>> InitializeTasks();
    }
}
