using AutoMapper;
using Publicon.Core.Entities.Concrete;
using Publicon.Infrastructure.Commands.Models.User;

namespace Publicon.Infrastructure.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterUserCommand, User>()
                .ForMember(m => m.Id, options => options.Ignore())
                .ForMember(m => m.IsActive, options => options.Ignore())
                .ForMember(m => m.HashedPassword, options => options.Ignore())
                .ForMember(m => m.RefreshToken, options => options.Ignore())
                .ForMember(m => m.SecurityCode, options => options.Ignore())
                .ForMember(m => m.SecurityCodeIssuedAt, options => options.Ignore())
                .ForMember(m => m.PasswordSecurityCode, options => options.Ignore())
                .ForMember(m => m.Role, options => options.Ignore());
        }
    }
}
