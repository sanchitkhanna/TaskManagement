using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.BLL.DTO.Task;
using TaskManagement.DAL.Repositories.Task;

namespace TaskManagement.BLL.Services.Task
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<TaskCreateRequest> _createValidator;
        private readonly IValidator<TaskUpdateRequest> _updateValidator;
        public TaskService(ITaskRepository taskRepository, IMapper mapper, IValidator<TaskCreateRequest> createValidator,
            IValidator<TaskUpdateRequest> updateValidator)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }
        public async Task<IEnumerable<TaskResponse>> GetTasks()
        {
            return _mapper.Map<IEnumerable<TaskResponse>>(await _taskRepository.GetTasks());
        }

        public async Task<TaskResponse> GetTaskById(Guid taskId)
        {
            return _mapper.Map<TaskResponse>(await _taskRepository.GetTaskById(taskId));
        }

        public async Task<bool> CreateTask(TaskCreateRequest taskCreateRequest)
        {
            if ((await _createValidator.ValidateAsync(taskCreateRequest)).IsValid)
            {
                var taskEntity = _mapper.Map<DAL.Entities.Task>(taskCreateRequest);
                await _taskRepository.CreateTask(taskEntity);
                return true;
            }
            return false;
        }
        public async Task<bool> UpdateTask(TaskUpdateRequest taskUpdateRequest)
        {
            if ((await _updateValidator.ValidateAsync(taskUpdateRequest)).IsValid)
            {
                var taskEntity = await _taskRepository.GetTaskById(taskUpdateRequest.TaskId);
                if(taskEntity != null)
                {
                    
                    return await _taskRepository.UpdateTask(_mapper.Map(taskUpdateRequest, taskEntity));
                }
                return false;
            }
            return false;
        }

        public async Task<bool> DeleteTask(Guid taskId)
        {
            var task = await _taskRepository.GetTaskById(taskId);
            if (task != null)
            {
                await _taskRepository.DeleteTask(task);
                return true;
            }
            return false;
        }
    }
}
