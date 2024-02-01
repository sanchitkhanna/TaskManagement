using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.DAL.Repositories.Task
{
    public interface ITaskRepository
    {
        Task<IEnumerable<DAL.Entities.Task>> GetTasks();
        Task<Entities.Task?> GetTaskById(Guid taskId);
        Task<Entities.Task> CreateTask(Entities.Task task);
        Task<bool> UpdateTask(Entities.Task task);
        Task<bool> DeleteTask(Entities.Task task);
    }
}
