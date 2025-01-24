using GestionDeTareas.Core.Domain.Enum;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeTareas.Core.Application.DTos
{
    public record CreateTaskDto
   (
       string DescriptionAboutTask,
       DateOnly DuaDate,
       Status StatusTask,
       int AdditionalData
   );
}
