using AutoMapper;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.BLL.DTO.Task;
using TaskManagement.BLL.Profiles;
using TaskManagement.BLL.Services.Task;
using TaskManagement.BLL.Validators.Task;
using TaskManagement.DAL.Repositories.Task;

namespace TaskManagement.BLL.Tests
{
    public class Tests
    {
        private static IMapper _mapper;
        private TaskService _taskService;
        private Mock<ITaskRepository> _taskRepository;

        [SetUp]
        public void Setup()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new TaskProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            _taskRepository = new Mock<ITaskRepository>();
            _taskService = new TaskService(_taskRepository.Object, _mapper, new TaskCreateValidator(), new TaskUpdateValidator());
        }

        [Test]
        public async Task GetTasksShouldReturnResultsOfTaskResponseType()
        {
            IEnumerable<DAL.Entities.Task> list = new List<DAL.Entities.Task>()
            {
                new DAL.Entities.Task()
                {
                    TaskId = Guid.NewGuid(),
                    Name = "Dummy Task",
                    Description = "Dummy Task"
                }
            };
            _taskRepository.Setup(x => x.GetTasks()).ReturnsAsync(list);

            var result = await _taskService.GetTasks();

            Assert.IsInstanceOf(typeof(IEnumerable<TaskResponse>), result);
        }

        [Test]
        public async Task CreateTaskShouldReturnTrue()
        {
            var taskCreateRequest = new TaskCreateRequest()
            {
                Name = "Dummy Task",
                Description = "Dummy Task"
            };
            var taskEntity = new DAL.Entities.Task()
            {
                TaskId = Guid.NewGuid(),
                Name = "Dummy Task",
                Description = "Dummy Task"
            };
            _taskRepository.Setup(x => x.CreateTask(taskEntity)).ReturnsAsync(taskEntity);

            var result = await _taskService.CreateTask(taskCreateRequest);

            Assert.True(result);
        }

        [Test]
        public async Task CreateTaskWithEmptyNameShouldReturnFalse()
        {
            var taskCreateRequest = new TaskCreateRequest()
            {
                Name = String.Empty,
                Description = "Dummy Task"
            };
            var taskEntity = new DAL.Entities.Task()
            {
                Name = String.Empty,
                Description = "Dummy Task"
            };
            _taskRepository.Setup(x => x.CreateTask(taskEntity)).ReturnsAsync(taskEntity);

            var result = await _taskService.CreateTask(taskCreateRequest);

            Assert.False(result);
        }

        [Test]
        public async Task UpdateTaskShouldReturnTrue()
        {
            var guid = Guid.NewGuid();
            var taskUpdateRequest = new TaskUpdateRequest()
            {
                TaskId = guid,
                Name = "Dummy Task Updated",
                Description = "Dummy Task Updated"
            };
            var taskEntity = new DAL.Entities.Task()
            {
                TaskId = guid,
                Name = "Dummy Task",
                Description = "Dummy Task"
            };
            _taskRepository.Setup(x => x.UpdateTask(taskEntity)).ReturnsAsync(true);
            _taskRepository.Setup(x => x.GetTaskById(guid)).ReturnsAsync(taskEntity);

            var result = await _taskService.UpdateTask(taskUpdateRequest);

            Assert.True(result);
        }

        [Test]
        public async Task DeleteTaskShouldReturnTrue()
        {
            var guid = Guid.NewGuid();
            var taskEntity = new DAL.Entities.Task()
            {
                TaskId = guid,
                Name = "Dummy Task",
                Description = "Dummy Task"
            };
            _taskRepository.Setup(x => x.DeleteTask(taskEntity)).ReturnsAsync(true);
            _taskRepository.Setup(x => x.GetTaskById(guid)).ReturnsAsync(taskEntity);

            var result = await _taskService.DeleteTask(guid);

            Assert.True(result);
        }

        [Test]
        public async Task DeleteTaskWithInvalidIdShouldReturnFalse()
        {
            var guid = Guid.NewGuid();
  
            _taskRepository.Setup(x => x.GetTaskById(guid)).ReturnsAsync((DAL.Entities.Task?)null);

            var result = await _taskService.DeleteTask(guid);

            Assert.False(result);
        }
    }
}