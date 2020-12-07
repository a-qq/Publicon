using FluentValidation;
using Publicon.Infrastructure.Queries.Models.Publication;

namespace Publicon.Api.Validators.Publication
{
    public class GetExcelDetailedRaportQueryValidator : AbstractValidator<GetExcelDetailedRaportQuery>
    {
        public GetExcelDetailedRaportQueryValidator()
        {
            RuleFor(p => p.CategoryId).NotNull().NotEmpty();
        }
    }
}
