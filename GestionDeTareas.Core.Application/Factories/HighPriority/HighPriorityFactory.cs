using GestionDeTareas.Core.Application.Interfaces.Factories;

namespace GestionDeTareas.Core.Application.Factories.HighPriority
{
    //Factory
    public abstract class HighPriorityFactory
    {
        public abstract IHighPriority CreateHighPriority();
    }
}
