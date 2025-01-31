using GestionDeTareas.Core.Application.DTos;
using GestionDeTareas.Core.Application.Interfaces.Service;
using GestionDeTareas.Core.Domain.Enum;
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
            if (!taskDto.IsSuccess)
            {
                return BadRequest(taskDto.Error);
            }
            
            return Ok(taskDto.Value);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancelationToken)
        {
            var taskItems = await _taskService.GetlAllAsync(cancelationToken);
            if (!taskItems.IsSuccess)
            {
                return BadRequest(taskItems.Error);
            }

            return Ok(taskItems.Value);
        }

        [HttpGet("filter/status/{status}")]
        public async Task<IActionResult> FilterByStatusAsync([FromRoute] Status status, CancellationToken cancellationToken)
        {
            var filter = await _taskService.FilterByStatus(status, cancellationToken);

            if (!filter.IsSuccess)
            {
                return BadRequest(filter.Error);
            }
            
            return Ok(filter.Value);
        }

        [HttpGet("filter/description/{description}")]
        public async Task<IActionResult> FilterByDescriptionAsync([FromRoute] string description,CancellationToken cancellationToken)
        {
            var filter = await _taskService.FilterByDescriptionAsync(description, cancellationToken);
            if (!filter.IsSuccess)
            {
                return BadRequest(filter.Error);
            }

            return Ok(filter.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var task = await _taskService.GetByIdAsync(id, cancellationToken);
            if (!task.IsSuccess)
            {
                return NotFound(task.Error);
            }

            return Ok(task.Value);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var task = await _taskService.DeleteAsync(id, cancellationToken);
            if (!task.IsSuccess)
            {
                return NotFound(task.Error);
            }

            return Ok(task.Value);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromQuery] Guid id, [FromBody] UpdateTaskDtos updateTask, CancellationToken cancellationToken)
        {
            var task = await _taskService.UpdateAsync(id, updateTask, cancellationToken);
            if(!task.IsSuccess)
            {
                return NotFound(task.Error);
            }

            return Ok(task.Value);

        }

        [HttpGet("{id}/days-left")]
        public async Task<IActionResult> GetCalculateDaysAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var taskDay = await _taskService.CalculateDayLeftAsync(id, cancellationToken);
            if (!taskDay.IsSuccess)
            {
                return NotFound(taskDay.Error);
            }

            return Ok(taskDay.Value);
        }
    }
}
