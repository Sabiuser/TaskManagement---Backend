using Microsoft.EntityFrameworkCore;
using TaskManagement.Context;
using TaskManagement.Model;

namespace TaskManagement.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskManagementDbContext _context;

        public TaskRepository(TaskManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetTaskItemsAsync()
        {
            var tasks = await _context.Tasks.AsNoTracking().ToListAsync();
            return tasks;
        }

        public async Task<TaskItem> GetTaskItemByIdAsync(int taskId)
        {
            return await _context.Tasks.AsNoTracking().FirstOrDefaultAsync(x => x.Id == taskId);
            
        }

        public async Task<TaskItem> CreateTaskAsync(TaskItem item)
        {
             await _context.Tasks.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<TaskItem> UpdateTaskItemAsync(TaskItem item)
        {
            //var updateTask = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == item.Id);
            //if (updateTask != null)
            //{
            //    updateTask.TaskName = item.TaskName;
            //    updateTask.Priority = item.Priority;
            //    updateTask.DueDate = item.DueDate;
            //}
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteTaskItemAsync(int taskId)
        {
            var deletedTask = await _context.Tasks.FindAsync(taskId);

            if (deletedTask != null)
            {
                _context.Tasks.Remove(deletedTask);
                _context.SaveChanges();
            }

            return true;
        }
    }
}
