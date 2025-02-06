using GestionDeTareas.Core.Application.Interfaces.Factories;

namespace GestionDeTareas.Core.Application.Factories.ThreeDayTask
{
    public abstract class ThreeDayTaskFactory
    {
        public abstract IThreeDay CreateTaskThreeDays();
    }
}
