using AutoMapper;
using Publicon.Core.Entities.Concrete;
using Publicon.Infrastructure.DTOs;

namespace Publicon.Infrastructure.Profiles
{
    public class PublicationFieldProfile : Profile
    {
        public PublicationFieldProfile()
        {
            CreateMap<PublicationField, PublicationFieldDTO>();

            CreateMap<PublicationFieldToManipulateDTO, PublicationField>()
                .ForMember(m => m.Id, options => options.Ignore());
        }
    }
}
