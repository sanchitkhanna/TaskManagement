using Microsoft.AspNetCore.Mvc;
using TaskManagement.BLL.DTO.Task;
using TaskManagement.BLL.Services.Task;

namespace TaskManagement.UI.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            this._taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _taskService.GetTasks();
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskCreateRequest request)
        {
            if (await _taskService.CreateTask(request))
            {
                return RedirectToAction("Index");
            }
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _taskService.GetTaskById(id);
            if (result != null)
            {
                TaskUpdateRequest request = new TaskUpdateRequest()
                {
                    TaskId = result.TaskId,
                    Name = result.Name,
                    Deadline = result.Deadline,
                    Description = result.Description
                };
                return View(request);
            }
            return RedirectToAction("Error", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(TaskUpdateRequest request)
        {
            if (await _taskService.UpdateTask(request))
            {
                return RedirectToAction("Index");
            }
            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _taskService.GetTaskById(id);

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTask(Guid taskId)
        {
            await _taskService.DeleteTask(taskId);
            
            return RedirectToAction("Index");            
        }
    }
}
