using GestionDeTareas.Presentation.Api.ExceptionHandling;

namespace GestionDeTareas.Presentation.Api.Extensions
{
    public static class AddExceptionHandler
    {
        public static void AddException(this IServiceCollection services)
        {
            services.AddExceptionHandler<GlobalExceptionHandler>();
        }
    }
}
