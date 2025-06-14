using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using urlshortener.Dtos.Url;

namespace urlshortener.Validators;

public class RedirectUrlDtoValidator : AbstractValidator<RedirectUrlDto>
{
    public RedirectUrlDtoValidator()
    {
        RuleFor(x => x.OriginalUrl)
            .NotEmpty().WithMessage("Original URL is required.")
            .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
            .WithMessage("Original URL must be a valid absolute URL (e.g., https://example.com)");
    }
}
