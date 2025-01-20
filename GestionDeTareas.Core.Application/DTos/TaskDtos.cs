using GestionDeTareas.Core.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeTareas.Core.Application.DTos
{
    public record TaskDtos
    (
        Guid Id,
        string Description,
        DateOnly DuaDate,
        Status Status,
        int AdditionalData
   );
}
