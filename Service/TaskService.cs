using TaskManagement.DTO;
using TaskManagement.Model;
using TaskManagement.Repository;

namespace TaskManagement.Service
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repo;

        public TaskService(ITaskRepository repo) { 
        _repo = repo;
        }

        public async Task<IEnumerable<TaskItem>> GetTaskItemsAsync()
        {
            var allTask = await _repo.GetTaskItemsAsync();
            return allTask;
        }

        public async Task<TaskItem> GetTaskByIdAsync(int id)
        {
            var taskById = await _repo.GetTaskItemByIdAsync(id);
            return taskById;
        }

        public async Task<TaskItem> CreateTaskAsync(CreateDTO task)
        {
            var newTask = new TaskItem
            {
                TaskName = task.TaskName,
                Priority = task.Priority,
                DueDate = task.DueDate,
            };
            
            return await _repo.CreateTaskAsync(newTask);
        }

        public async Task<TaskItem> UpdateTaskAsync(int id, UpdateDTO task)
        {
            var existingTask = await _repo.GetTaskItemByIdAsync(id);

            if (existingTask == null)
            {
                return null;
            }

            existingTask.TaskName = task.TaskName;
            existingTask.Priority = task.Priority;
            existingTask.DueDate = task.DueDate;

            return await _repo.UpdateTaskItemAsync(existingTask);
        }


        public async Task<bool> DeleteTaskById(int id)
        {
            var deleteTask = await _repo.DeleteTaskItemAsync(id);
            return deleteTask;
        }
    }
}
