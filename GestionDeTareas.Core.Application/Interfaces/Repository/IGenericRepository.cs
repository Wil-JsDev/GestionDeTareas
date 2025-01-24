using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeTareas.Core.Application.Interfaces.Repository
{
    /// <summary>
    ///  TEntity debe de ser una clase
    /// </summary>
    public interface IGenericRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);

        Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task CreateAsync(TEntity entity, CancellationToken cancellationToken);

        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);

        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
