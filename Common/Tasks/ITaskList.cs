
namespace Common.Tasks
{
    public interface ITaskList<TId> where TId : struct
    {
        ITaskInfo<TId> GetTask(TId id);
        IList<ITaskInfo<TId>> GetTasks();
        protected List<ITaskInfo<TId>> InitializeTasks();
    }
}