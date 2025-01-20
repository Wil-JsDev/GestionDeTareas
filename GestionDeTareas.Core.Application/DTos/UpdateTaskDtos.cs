using GestionDeTareas.Core.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeTareas.Core.Application.DTos
{
    public record UpdateTaskDtos
    (
         string Description,
         Status Status
    );
}
