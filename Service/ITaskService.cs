using System.Collections;
using TaskManagement.DTO;
using TaskManagement.Model;

namespace TaskManagement.Service
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItem>> GetTaskItemsAsync();
        Task<TaskItem> GetTaskByIdAsync(int id);
        Task<TaskItem> CreateTaskAsync(CreateDTO item);

        Task<TaskItem> UpdateTaskAsync(int id, UpdateDTO task);

        Task<bool> DeleteTaskById(int id);
    }
}
