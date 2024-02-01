using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.BLL.DTO.Task;

namespace TaskManagement.BLL.Services.Task
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskResponse>> GetTasks();
        Task<TaskResponse> GetTaskById(Guid taskId);
        Task<bool> CreateTask(TaskCreateRequest taskCreateRequest);
        Task<bool> UpdateTask(TaskUpdateRequest taskUpdateRequest);
        Task<bool> DeleteTask(Guid taskId);
    }
}
