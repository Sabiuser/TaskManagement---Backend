using System.Collections;
using TaskManagement.Model;

namespace TaskManagement.Repository
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetTaskItemsAsync();

        Task<TaskItem> GetTaskItemByIdAsync(int taskId);

        Task<TaskItem> CreateTaskAsync(TaskItem item);

        Task<TaskItem> UpdateTaskItemAsync(TaskItem item);

        Task <bool> DeleteTaskItemAsync(int taskId);
    }
}
