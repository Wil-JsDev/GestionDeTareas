using GestionDeTareas.Core.Application.Interfaces.Factories;

namespace GestionDeTareas.Core.Application.Factories.HighPriority
{
    //Concrete Factory
    public class ConcreteHighPriorityFactory : HighPriorityFactory
    {
        public override IHighPriority CreateHighPriority()
        {
            return new HighPriorityTask();
        }
    }
}
