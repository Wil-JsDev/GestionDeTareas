using GestionDeTareas.Core.Application.DTos;
using GestionDeTareas.Core.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeTareas.Core.Application.Interfaces.Service
{
    public interface ITaskService
    {
        Task<TaskDtos> CreateAsync(CreateTaskDto task, CancellationToken cancellationToken);

        Task<TaskDtos> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<IEnumerable<TaskDtos>> GetlAllAsync(CancellationToken cancellationToken);

        Task<IEnumerable<TaskDtos>> FilterByStatus(Status status, CancellationToken cancellationToken);

        Task<TaskDtos> DeleteAsync(Guid id, CancellationToken cancellationToken);

        Task<TaskDtos> UpdateAsync(Guid id, UpdateTaskDtos updateTaskDtos, CancellationToken cancellationToken);
    }
}
