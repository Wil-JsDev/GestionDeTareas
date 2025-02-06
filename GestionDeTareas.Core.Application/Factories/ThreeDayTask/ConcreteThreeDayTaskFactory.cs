
using GestionDeTareas.Core.Application.Interfaces.Factories;

namespace GestionDeTareas.Core.Application.Factories.ThreeDayTask
{
    public class ConcreteThreeDayTaskFactory : ThreeDayTaskFactory
    {
        public override IThreeDay CreateTaskThreeDays()
        {
            return new ThreeDayTask();
        }
    }
}
