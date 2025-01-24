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
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly GestionDeTareasContext _context;

        public GenericRepository(GestionDeTareasContext context)
        {
            _context = context;
        }

        public virtual async Task SaveChangesAsync(CancellationToken cancellationToken) => 
            await _context.SaveChangesAsync(cancellationToken);

        public virtual async Task CreateAsync(T entity, CancellationToken cancellationToken)
        {
            await _context.Set<T>().AddAsync(entity,cancellationToken); 
            await SaveChangesAsync(cancellationToken);
        }

        public virtual async Task DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            _context.Set<T>().Remove(entity);
            await SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken) => 
            await _context.Set<T>().ToListAsync(cancellationToken);

        public virtual async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
           T? t = await _context.Set<T>()
                                .FindAsync(id, cancellationToken);
            return t;
        }

        public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            _context.Set<T>().Attach(entity);
            _context.Set<T>().Entry(entity).State = EntityState.Modified;
            await SaveChangesAsync(cancellationToken);
        }
    }
}
