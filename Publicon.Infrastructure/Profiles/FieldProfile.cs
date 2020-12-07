using AutoMapper;
using Publicon.Core.Entities.Concrete;
using Publicon.Infrastructure.Commands.Models.Category;
using Publicon.Infrastructure.DTOs;

namespace Publicon.Infrastructure.Profiles
{
    public class FieldProfile : Profile
    {
        public FieldProfile()
        {
            CreateMap<FieldToAddDTO, Field>()
                .ForMember(m => m.Id, options => options.Ignore())
                .ForMember(m => m.CategoryId, options => options.Ignore());

            CreateMap<Field, FieldDTO>();

            CreateMap<EditFieldCommand, Field>()
                .ForMember(m => m.Id, options => options.Ignore())
                .ForMember(m => m.CategoryId, options => options.Ignore());

            CreateMap<CreateFieldCommand, Field>()
                .ForMember(m => m.Id, options => options.Ignore())
                .ForMember(m => m.CategoryId, options => options.Ignore());
        }
    }
}
