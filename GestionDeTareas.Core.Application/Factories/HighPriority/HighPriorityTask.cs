using GestionDeTareas.Core.Domain.Enum;
using GestionDeTareas.Core.Domain.Models;

namespace GestionDeTareas.Core.Application.Factories.HighPriority
{

    public class HighPriorityTask : HighPriorityFactory
    {
        public override TaskItem CreateHighPriorityTask(string description)
        {
            return new TaskItem()
            {
                Id = Guid.NewGuid(),
                Description = description,
                DuaDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                Status = Status.Pending,
                AdditionalData = 3
            };
        }
    }
}
