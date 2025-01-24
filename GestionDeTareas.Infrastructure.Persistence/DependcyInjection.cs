using GestionDeTareas.Core.Application.Interfaces.Repository;
using GestionDeTareas.Infrastructure.Persistence.Context;
using GestionDeTareas.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GestionDeTareas.Infrastructure.Persistence
{
    public static class DependcyInjection
    {
        public static void AddPersistence(this IServiceCollection service, IConfiguration configuration)
        {
            #region Context
            service.AddDbContext<GestionDeTareasContext>(s =>
            {
                s.UseSqlServer(configuration.GetConnectionString("GestionDeTareas"), 
                    b => b.MigrationsAssembly("GestionDeTareas.Infrastructure.Persistence"));
            });
            #endregion

            #region Repositories
            service.AddTransient(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            service.AddTransient<ITaskRepository, TaskRepository>();    
            #endregion
        }
    }
}
