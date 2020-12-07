using FluentValidation;
using Publicon.Infrastructure.Commands.Models.Notification;
using System;

namespace Publicon.Api.Validators.Notification
{
    public class SetNotificationCommandValidator : AbstractValidator<SetNotificationCommand>
    {
        public SetNotificationCommandValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(300);
            RuleFor(x => x.Message).NotNull().MaximumLength(25000);
            RuleFor(x => x.SendTimeInUTC).NotNull()
                .Must(d => d.Date.AddHours(d.Hour).AddSeconds(30) > DateTime.UtcNow)
                .WithMessage("Sendtime must be greater then current time");
        }
    }
}
