using GestionDeTareas.Core.Domain.Models;

namespace GestionDeTareas.Core.Application.Factories.ThreeDayTask
{
    public abstract class ThreeDayTaskFactory
    {
        public abstract TaskItem CreateTaskThreeDays(string description);
    }
}
