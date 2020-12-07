using AutoMapper;
using Publicon.Core.Entities.Concrete;
using Publicon.Infrastructure.Commands.Models.Notification;

namespace Publicon.Infrastructure.Profiles
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<SetNotificationCommand, Notification>()
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                .ForMember(dst => dst.SendTime, opt => opt.MapFrom(src => src.SendTimeInUTC.Date.AddHours(src.SendTimeInUTC.Hour)));
        }
    }
}
