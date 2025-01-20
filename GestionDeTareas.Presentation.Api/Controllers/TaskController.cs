using GestionDeTareas.Core.Application.DTos;
using GestionDeTareas.Core.Application.Interfaces.Service;
using GestionDeTareas.Core.Domain.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionDeTareas.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/task")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTaskDto createTaskDtos, CancellationToken cancellationToken)
        {
            var taskDto = await _taskService.CreateAsync(createTaskDtos, cancellationToken);

            return taskDto == null ? BadRequest() : Ok(taskDto);
        }

        [HttpGet]
        public async Task<IEnumerable<TaskDtos>> GetAllAsync(CancellationToken cancelationToken) =>
           await _taskService.GetlAllAsync(cancelationToken);

        [HttpGet("{status}")]
        public async Task<IActionResult> FilterByStatusAsync([FromRoute] Status status, CancellationToken cancellationToken)
        {
            var filter = await _taskService.FilterByStatus(status, cancellationToken);

            return filter == null ? BadRequest() : Ok(filter);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var task = await _taskService.GetByIdAsync(id, cancellationToken);
            return task == null ? NotFound($" {id} not found") : Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var task = await _taskService.DeleteAsync(id, cancellationToken);
            return task == null ? NotFound($"{id} not found") : NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromQuery] Guid id, [FromBody] UpdateTaskDtos updateTask, CancellationToken cancellationToken)
        {
            var task = await _taskService.UpdateAsync(id, updateTask, cancellationToken);
            return task == null ? NotFound() : Ok(task);
        }

    }
}
