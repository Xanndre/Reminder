using AutoMapper;
using Reminder.Core.DTOs;
using Reminder.Core.Entities;

namespace Reminder.Core.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TodoDTO, Todo>()
                .ForMember(c => c.Date, d => d.MapFrom(e => e.Date.ToLocalTime()))
                .ReverseMap();

            CreateMap<NotificationDTO, Notification>()
                .ForMember(c => c.Date, d => d.MapFrom(e => e.Date.ToLocalTime()))
                .ReverseMap();
        }
    }
}
