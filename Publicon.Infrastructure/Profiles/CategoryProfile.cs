using AutoMapper;
using Publicon.Core.Entities.Concrete;
using Publicon.Infrastructure.Commands.Models.Category;
using Publicon.Infrastructure.DTOs;

namespace Publicon.Infrastructure.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            ShouldMapField = fieldInfo => true;
            ShouldMapProperty = propertyInfo => true;

            CreateMap<CreateCategoryCommand, Category>()
                .ForMember(m => m.Id, options => options.Ignore())
                .ForMember(m => m.IsArchived, options => options.Ignore())
                .ForMember(m => m.Fields, options => options.Ignore());

            CreateMap<Category, CategoryDTO>();

            CreateMap<EditCategoryCommand, Category>()
                .ForMember(m => m.Id, options => options.Ignore());
        }
    }
}
