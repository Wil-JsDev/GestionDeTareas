using AutoMapper;
using GestionDeTareas.Core.Application.DTos;
using GestionDeTareas.Core.Application.Factories.HighPriority;
using GestionDeTareas.Core.Application.Factories.ThreeDayTask;
using GestionDeTareas.Core.Application.Helper;
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
        private readonly TaskHelper _taskHelper;
        private HighPriorityTask _factory;
        private ThreeDayTask _taskFactory; 
 
        public TaskService(
            ITaskRepository taskRepository, 
            IMapper mapper, 
            TaskHelper taskHelper,
            HighPriorityTask factory,
            ThreeDayTask taskFactory )
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
            _taskHelper = taskHelper;
            _factory = factory;
            _taskFactory = taskFactory;
        }

        public async Task<ResultT<TaskDtos>> CreateHighPriorityTask(
            string description,
            CancellationToken cancellationToken)
        {
            var exists = await _taskRepository.ValidateAsync(x => x.Description == description);
            if (!exists)
            {
                var taskItem = _factory.CreateHighPriorityTask(description);
                
                await _taskRepository.CreateAsync(taskItem,cancellationToken);

                _taskHelper.SendNotification(taskItem);

                TaskDtos dto = new
                (
                    Id: taskItem.Id,
                    Description: taskItem.Description,
                    DuaDate: taskItem.DuaDate,
                    Status: taskItem.Status,
                    AdditionalData: taskItem.AdditionalData
                );

                return ResultT<TaskDtos>.Success(dto);
            }

            return ResultT<TaskDtos>.Failure(Error.Failure("400", "Task description already exists"));
        }

        public async Task<ResultT<TaskDtos>> ThreeDaysTask(string description, CancellationToken cancellationToken)
        {
            var exists = await _taskRepository.ValidateAsync(x => x.Description == description);
            if (!exists)
            {
                var taskItem = _taskFactory.CreateTaskThreeDays(description);
                
                await _taskRepository.CreateAsync(taskItem,cancellationToken);
                _taskHelper.SendNotification(taskItem);

                TaskDtos taskDto =  new
                (
                    Id: taskItem.Id,
                    Description: taskItem.Description,
                    DuaDate: taskItem.DuaDate,
                    Status: taskItem.Status,
                    AdditionalData: taskItem.AdditionalData
                );
                return ResultT<TaskDtos>.Success(taskDto);
            }

            return ResultT<TaskDtos>.Failure(Error.Failure("400", "Task description already exists"));
        }

        public async Task<ResultT<TaskDtos>> CreateAsync(CreateTaskDto createTaskDto, CancellationToken cancellationToken)
        {
            var task = _mapper.Map<TaskItem>(createTaskDto);
            if (task != null)
            {
                var exists = await _taskRepository.ValidateAsync(x => x.Description == task.Description);
                if (exists)
                {
                    return ResultT<TaskDtos>.Failure(Error.Failure("400", "Task description already exists."));
                }

                var isValid = _taskHelper.Validate(task);
                if (!isValid)
                {
                    return ResultT<TaskDtos>.Failure(Error.Failure("400", "Task is null or description is empty."));
                }

                await _taskRepository.CreateAsync(task, cancellationToken);

                _taskHelper.SendNotification(task);

                var taskDto = _mapper.Map<TaskDtos>(task); 
                return ResultT<TaskDtos>.Success(taskDto);
            }

            return ResultT<TaskDtos>.Failure(Error.Failure("400", "Failed to create task."));
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

        public async Task<ResultT<IEnumerable<TaskDtos>>> FilterByDescriptionAsync(string description, CancellationToken cancellationToken)
        {
            var filterDescription = await _taskRepository.GetFilterAsync(x => x.Description == description, cancellationToken);
            if (!filterDescription.Any())
            {
                return ResultT<IEnumerable<TaskDtos>>.Failure(Error.Failure("400", "The list is empty"));
            }

            var taskDto = filterDescription.Select(x => _mapper.Map<TaskDtos>(x));

            return ResultT<IEnumerable<TaskDtos>>.Success(taskDto);
        }
        
        public async Task<ResultT<TaskDayDto>> CalculateDayLeftAsync(Guid id, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(id, cancellationToken);

            if (task != null)
            {
                var daysLeft = _taskHelper.CalculateDaysLeft(task);
                TaskDayDto taskDayDto = new
                (
                    TaskId: task.Id,
                    Description: task.Description,
                    DuaDate: task.DuaDate,
                    Status: task.Status,
                    DayLeft: daysLeft,
                    AdditionalData: task.AdditionalData
                );

                return ResultT<TaskDayDto>.Success(taskDayDto);

            }

            return ResultT<TaskDayDto>.Failure(Error.Failure("404", $"{id} not found"));

        }

    }
}
