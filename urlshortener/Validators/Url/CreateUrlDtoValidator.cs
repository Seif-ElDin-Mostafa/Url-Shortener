using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using urlshortener.Dtos.Url;

namespace urlshortener.Validators;

public class CreateUrlDtoValidator : AbstractValidator<CreateUrlDto>
{
    public CreateUrlDtoValidator()
    {
        RuleFor(x => x.OriginalUrl)
            .NotEmpty().WithMessage("Original URL is required.")
            .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
            .WithMessage("Original URL must be a valid absolute URL (e.g., https://example.com)");

        RuleFor(x => x.CreatedAt)
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("CreatedAt cannot be in the future.");

        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("UserId must be greater than 0.");
    }
}
