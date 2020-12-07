using FluentValidation;
using Publicon.Infrastructure.Commands.Models.Publication;

namespace Publicon.Api.Validators.Publication
{
    public class UpsertPublicationFileCommandValidator : AbstractValidator<UpsertPublicationFileCommand>
    {
        public UpsertPublicationFileCommandValidator()
        {
            RuleFor(p => p.File).NotNull();
            RuleFor(p => p.File).Must(p => p.Length < 78643200).WithMessage("Max allowed file size is 75 MB");
        }
    }
}
