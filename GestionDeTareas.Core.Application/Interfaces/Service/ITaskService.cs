using GestionDeTareas.Core.Application.DTos;
using GestionDeTareas.Core.Domain.Enum;
using GestionDeTareas.Core.Domain.Utils;

namespace GestionDeTareas.Core.Application.Interfaces.Service
{
    public interface ITaskService
    {
        Task<ResultT<TaskDtos>> CreateAsync(CreateTaskDto task, CancellationToken cancellationToken);

        Task <ResultT<TaskDtos>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<ResultT<IEnumerable<TaskDtos>>> GetlAllAsync(CancellationToken cancellationToken);

        Task<ResultT<IEnumerable<TaskDtos>>> FilterByStatus(Status status, CancellationToken cancellationToken);

        Task<ResultT<TaskDtos>> DeleteAsync(Guid id, CancellationToken cancellationToken);

        Task<ResultT<TaskDtos>> UpdateAsync(Guid id, UpdateTaskDtos updateTaskDtos, CancellationToken cancellationToken);

        Task<ResultT<IEnumerable<TaskDtos>>> FilterByDescriptionAsync(string description, CancellationToken cancellationToken);

        Task<ResultT<TaskDayDto>> CalculateDayLeftAsync(Guid id, CancellationToken cancellationToken);
    }
}
