using GestionDeTareas.Core.Application.Factories.HighPriority;
using GestionDeTareas.Core.Application.Factories.ThreeDayTask;
using GestionDeTareas.Core.Application.Helper;
using GestionDeTareas.Core.Application.Interfaces.Service;
using GestionDeTareas.Core.Application.Mapper;
using GestionDeTareas.Core.Application.Service;
using Microsoft.Extensions.DependencyInjection;

namespace GestionDeTareas.Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection service)
        {
            service.AddAutoMapper(typeof(MappingProfile));

            #region Services
            service.AddScoped<ITaskService, TaskService>();
            service.AddScoped<TaskHelper>();
            service.AddScoped<HighPriorityTask>();
            service.AddScoped<ThreeDayTask>();
            #endregion

            return service;
        }
    }
}
