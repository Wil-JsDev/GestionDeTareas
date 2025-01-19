using GestionDeTareas.Core.Domain.Enum;

namespace GestionDeTareas.Core.Domain.Models
{
    public sealed class TaskItem
    {
        public Guid Id { get; set; }

        public string? Description { get; set; }

        public DateOnly DuaDate { get; set; }

        public Status Status { get; set; }

        public int AdditionalData { get; set; }
    }
}
