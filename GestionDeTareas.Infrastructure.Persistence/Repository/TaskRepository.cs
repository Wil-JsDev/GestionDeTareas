using GestionDeTareas.Core.Application.Interfaces.Repository;
using GestionDeTareas.Core.Domain.Models;
using GestionDeTareas.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeTareas.Infrastructure.Persistence.Repository
{
    public class TaskRepository : GenericRepository<TaskItem>, ITaskRepository
    {
        public TaskRepository(GestionDeTareasContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TaskItem>> GetFilterAsync(
            Func<TaskItem, bool> predicate, 
            CancellationToken cancellationToken)
        {
            var query = await Task.Run ( () => _context.Set<TaskItem>()
                                      .AsNoTracking()
                                      .Where(predicate));
            
            return query;                         
        }
    }
}
