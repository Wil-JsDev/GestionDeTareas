using GestionDeTareas.Core.Domain.Enum;
using GestionDeTareas.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeTareas.Core.Application.Interfaces.Repository
{
    public interface ITaskRepository : IGenericRepository<TaskItem>
    {
        Task<IEnumerable<TaskItem>> GetFilterAsync( Func<TaskItem, bool> predicate, CancellationToken cancellationToken);
    }
}
