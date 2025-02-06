using GestionDeTareas.Core.Domain.Enum;
using GestionDeTareas.Core.Domain.Models;

namespace GestionDeTareas.Core.Application.Factories.ThreeDayTask
{
    public class ThreeDayTask : ThreeDayTaskFactory
    {
        public override TaskItem CreateTaskThreeDays(string description)
        {
            return new TaskItem
            {
                Id = Guid.NewGuid(),
                Description = description,
                DuaDate = DateOnly.FromDateTime(DateTime.Now.AddDays(3)),
                Status = Status.Pending,
                AdditionalData = 3
            };
        }
    }
}
