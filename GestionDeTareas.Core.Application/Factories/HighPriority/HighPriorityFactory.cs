using GestionDeTareas.Core.Domain.Models;

namespace GestionDeTareas.Core.Application.Factories.HighPriority
{
    public abstract class HighPriorityFactory
    {
        public abstract TaskItem CreateHighPriorityTask(string description);
    }
}
