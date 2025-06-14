using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using urlshortener.Dtos.Url;

namespace urlshortener.Validators;

public class UpdateUrlDtoValidator : AbstractValidator<UpdateUrlDto>
{
    public UpdateUrlDtoValidator()
    {
        RuleFor(x => x.ShortenedUrl)
            .NotEmpty().WithMessage("Shortened URL is required.")
            .Matches("^[a-zA-Z0-9_-]{5,20}$") // Example: 5-20 chars, alphanumeric, dash/underscore
            .WithMessage("Shortened URL must be 5–20 characters and contain only letters, numbers, dashes or underscores.");

        RuleFor(x => x.OriginalUrl)
            .NotEmpty().WithMessage("Original URL is required.")
            .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
            .WithMessage("Original URL must be a valid absolute URL (e.g., https://example.com)");

        RuleFor(x => x.CreatedAt)
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("CreatedAt cannot be in the future.");
    }
}
