using GestionDeTareas.Core.Domain.Models;

namespace GestionDeTareas.Core.Application.Interfaces.Repository
{
    public interface ITaskRepository : IGenericRepository<TaskItem>
    {
        Task<IEnumerable<TaskItem>> GetFilterAsync( Func<TaskItem, bool> predicate, CancellationToken cancellationToken);

        Task<bool> ValidateAsync(Func<TaskItem, bool> validate);

    }
}
