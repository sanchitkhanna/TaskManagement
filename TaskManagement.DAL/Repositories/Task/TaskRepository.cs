using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.DAL.Repositories.Task
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskManagementDbContext _dbContext;
        public TaskRepository(TaskManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Entities.Task>> GetTasks()
        {
            return await _dbContext.Tasks.ToListAsync();
        }

        public async Task<Entities.Task?> GetTaskById(Guid taskId)
        {
            return await _dbContext.Tasks.FirstOrDefaultAsync(x => x.TaskId == taskId);
        }
        public async Task<Entities.Task> CreateTask(Entities.Task task)
        {
            if(task == null) { throw new ArgumentNullException(nameof(task)); }
            await _dbContext.Tasks.AddAsync(task);
            await _dbContext.SaveChangesAsync();
            return task;
        }

        public async Task<bool> UpdateTask(Entities.Task task)
        {
            if (task == null) { throw new ArgumentNullException(nameof(task)); }
            _dbContext.Tasks.Update(task);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteTask(Entities.Task task)
        {
            if (task == null) { throw new ArgumentNullException(nameof(task)); }
            _dbContext.Tasks.Remove(task);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
