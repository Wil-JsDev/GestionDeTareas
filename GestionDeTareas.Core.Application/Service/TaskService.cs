using AutoMapper;
using GestionDeTareas.Core.Application.DTos;
using GestionDeTareas.Core.Application.Interfaces.Repository;
using GestionDeTareas.Core.Application.Interfaces.Service;
using GestionDeTareas.Core.Domain.Enum;
using GestionDeTareas.Core.Domain.Models;
using GestionDeTareas.Core.Domain.Utils;

namespace GestionDeTareas.Core.Application.Service
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        public TaskService(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<ResultT<TaskDtos>> CreateAsync(CreateTaskDto createTaskDto, CancellationToken cancellationToken)
        {
            var task = _mapper.Map<TaskItem>(createTaskDto);
            if (task != null)
            {
                await _taskRepository.CreateAsync(task, cancellationToken);
                var taskDto = _mapper.Map<TaskDtos>(task); 
                return ResultT<TaskDtos>.Success(taskDto);
            }

            return ResultT<TaskDtos>.Failure(Error.Failure("400", ""));
        }

        public async Task<ResultT<TaskDtos>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(id, cancellationToken);
            if (task != null)
            {
               var taskDto = _mapper.Map<TaskDtos>(task);
               return ResultT<TaskDtos>.Success(taskDto);
            }

            return ResultT<TaskDtos>.Failure(Error.NotFound("404", $"{id} not found"));
        }

        public async Task<ResultT<TaskDtos>> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var taskItem = await _taskRepository.GetByIdAsync(id, cancellationToken);
            if (taskItem != null)
            {
                await _taskRepository.DeleteAsync(taskItem, cancellationToken);
                var taskDto = _mapper.Map<TaskDtos>(taskItem);

                return ResultT<TaskDtos>.Success(taskDto);
            }

            return ResultT<TaskDtos>.Failure(Error.NotFound("404", $"{id} not found"));

        }

        public async Task<ResultT<IEnumerable<TaskDtos>>> FilterByStatus(Status status, CancellationToken cancellationToken)
        {
            var taskItems = await _taskRepository.GetFilterAsync(s => s.Status == status,cancellationToken);
            if (!taskItems.Any())
            {
                return ResultT<IEnumerable<TaskDtos>>.Failure(Error.Failure("400","The list is empty"));
            }

            var taskDto = taskItems.Select(t => _mapper.Map<TaskDtos>(t));

            return ResultT<IEnumerable<TaskDtos>>.Success(taskDto);

        }

        public async Task<ResultT<IEnumerable<TaskDtos>>> GetlAllAsync(CancellationToken cancellationToken)
        {
            var taskItems = await _taskRepository.GetAllAsync(cancellationToken);
            if (!taskItems.Any())
            {
                return ResultT<IEnumerable<TaskDtos>>.Failure(Error.Failure("400", "The list is empty"));
            }
            var taskDto = taskItems.Select(t => _mapper.Map<TaskDtos>(t));

            return ResultT<IEnumerable<TaskDtos>>.Success(taskDto);

        }
        public async Task<ResultT<TaskDtos>> UpdateAsync(Guid id, UpdateTaskDtos updateTaskDtos, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(id,cancellationToken);
            if (task != null)
            {
                task = _mapper.Map<UpdateTaskDtos, TaskItem>(updateTaskDtos, task);
                await _taskRepository.UpdateAsync(task, cancellationToken);
                
                var taskDto = _mapper.Map<TaskDtos>(task);

                return ResultT<TaskDtos>.Success(taskDto);
            }

            return ResultT<TaskDtos>.Failure(Error.NotFound("404", $"{id} not found"));
        }
    }
}
