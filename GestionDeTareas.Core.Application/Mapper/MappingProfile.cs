using AutoMapper;
using GestionDeTareas.Core.Application.DTos;
using GestionDeTareas.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeTareas.Core.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateTaskDto, TaskItem>()
                    .ForMember(dest => dest.Description, src => src.MapFrom(x => x.DescriptionAboutTask))
                    .ForMember(src => src.Status, dest => dest.MapFrom(x => x.StatusTask));

            CreateMap<TaskItem, TaskDtos>();

            CreateMap<UpdateTaskDtos, TaskItem>();
        }
    }
}
