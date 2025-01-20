using AutoMapper;
using GestionDeTareas.Core.Application.DTos;
using GestionDeTareas.Core.Application.Interfaces.Repository;
using GestionDeTareas.Core.Application.Interfaces.Service;
using GestionDeTareas.Core.Domain.Enum;
using GestionDeTareas.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<TaskDtos> CreateAsync(CreateTaskDto taskDto, CancellationToken cancellationToken)
        {
            var task = _mapper.Map<TaskItem>(taskDto);
            if (task != null)
            {
                await _taskRepository.CreateAsync(task, cancellationToken);
                return _mapper.Map<TaskDtos>(task);
            }

            return null;
        }

        public async Task<TaskDtos> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(id, cancellationToken);
            if (task != null)
            {
               var taskDto = _mapper.Map<TaskDtos>(task);
               return taskDto;
            }

            return null;    
        }

        public async Task<TaskDtos> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var taskItem = await _taskRepository.GetByIdAsync(id, cancellationToken);
            if (taskItem != null)
            {
                await _taskRepository.DeleteAsync(taskItem, cancellationToken);
                return _mapper.Map<TaskDtos>(taskItem);
            }
            return null;
        }

        public async Task<IEnumerable<TaskDtos>> FilterByStatus(Status status, CancellationToken cancellationToken)
        {
            var taskItems = await _taskRepository.GetFilterAsync(s => s.Status == status,cancellationToken);
            return taskItems.Select(t => _mapper.Map<TaskDtos>(t));
        }

        public async Task<IEnumerable<TaskDtos>> GetlAllAsync(CancellationToken cancellationToken)
        {
            var taskItems = await _taskRepository.GetAllAsync(cancellationToken);
            return taskItems.Select(t => _mapper.Map<TaskDtos>(t));
        }
        public async Task<TaskDtos> UpdateAsync(Guid id, UpdateTaskDtos updateTaskDtos, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(id,cancellationToken);
            if (task != null)
            {
                task = _mapper.Map<UpdateTaskDtos, TaskItem>(updateTaskDtos, task);
                await _taskRepository.UpdateAsync(task, cancellationToken);
                return _mapper.Map<TaskDtos>(task);
            }
            
            return null;
        }
    }
}
