using GestionDeTareas.Core.Domain.Enum;

namespace GestionDeTareas.Core.Application.DTos
{
    public sealed record TaskDayDto
    (
        Guid TaskId,
        string Description,
        DateOnly DuaDate,
        Status Status,
        int DayLeft,
        int AdditionalData
    );
}
