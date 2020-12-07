using FluentValidation;
using Publicon.Infrastructure.Queries.Models.Publication;

namespace Publicon.Api.Validators.Publication
{
    public class GetPublicationsQueryValidator : AbstractValidator<GetPublicationsQuery>
    {
        public GetPublicationsQueryValidator()
        {
            When(x => !(x.PageNumber == 0 && x.PageSize == 0), () =>
              {
                  RuleFor(x => x.PageNumber).GreaterThan(0);
                  RuleFor(x => x.PageSize).GreaterThan(0);
              });
        }
    }
}
