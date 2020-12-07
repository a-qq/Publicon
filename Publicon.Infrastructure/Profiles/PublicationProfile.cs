using AutoMapper;
using Publicon.Core.Entities.Concrete;
using Publicon.Infrastructure.Commands.Models.Publication;
using Publicon.Infrastructure.DTOs;
using System;

namespace Publicon.Infrastructure.Profiles
{
    public class PublicationProfile : Profile
    {
        public PublicationProfile()
        {
            CreateMap<CreatePublicationCommand, Publication>()
                .ForMember(m => m.Id, options => options.Ignore())
                .ForMember(m => m.PublicationFields, options => options.Ignore())
                .ForMember(m => m.UserId, options => options.Ignore())
                .ForMember(m => m.FileName, options => options.Ignore())
                .ForMember(m => m.PublicationTime, options => options.MapFrom(x=>x.PublicationTime.Date))
                .ForMember(m => m.AddedAt, options => options.MapFrom(x => DateTime.UtcNow))
                .ForMember(m => m.FileName, options => 
                { 
                    options.PreCondition(x => x.File != null);
                    options.MapFrom(x => Guid.NewGuid().ToString());
                });

            CreateMap<EditPublicationCommand, Publication>()
                .ForMember(m => m.Id, options => options.Ignore())
                .ForMember(m => m.CategoryId, options => options.Ignore())
                .ForMember(m => m.PublicationFields, options => options.Ignore())
                .ForMember(m => m.FileName, options => options.Ignore())
                .ForMember(m => m.AddedAt, options => options.Ignore())
                .ForMember(m => m.FileName, options => options.Ignore());

            CreateMap<Publication, PublicationDetailsDTO>()
                .ForMember(m => m.UserFamilyName, opt => opt.MapFrom(m => m.User.FamilyName))
                .ForMember(m => m.UserGivenName, opt => opt.MapFrom(m => m.User.GivenName))
                .ForMember(m => m.UserId, opt => opt.MapFrom(m => m.User.Id))
                .ForMember(m => m.HasUploadedFile, opt => opt.MapFrom(m => !string.IsNullOrWhiteSpace(m.FileName)));

            CreateMap<Publication, PublicationDTO>();
        }
    }
}
