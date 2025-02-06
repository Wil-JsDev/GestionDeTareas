namespace GestionDeTareas.Core.Application.Interfaces.Factories
{
    public interface IThreeDay
    {
        void SetDays(DateOnly dateOnly);

        DateOnly GetDays();
    }
}
