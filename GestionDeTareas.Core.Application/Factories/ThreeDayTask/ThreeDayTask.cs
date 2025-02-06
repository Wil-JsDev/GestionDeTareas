using GestionDeTareas.Core.Application.Interfaces.Factories;

namespace GestionDeTareas.Core.Application.Factories.ThreeDayTask
{
    public class ThreeDayTask : IThreeDay
    {
        private DateOnly _date;

        public void SetDays(DateOnly date)
        {
            _date = date;
        }

        public DateOnly GetDays() => _date;
    }
}
