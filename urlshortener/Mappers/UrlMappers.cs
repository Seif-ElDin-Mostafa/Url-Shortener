using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using urlshortener.Dtos.Url;
using urlshortener.Models;

namespace urlshortener.Mappers;

public static class UrlMappers
{
    public static Url ToCreateUrl(this CreateUrlDto createUrlDto)
    {
        return new Url
        {
            OriginalUrl = createUrlDto.OriginalUrl,
            CreatedAt = createUrlDto.CreatedAt,
            UserId = createUrlDto.UserId
        };
    }

    public static RedirectUrlDto ToRedirectUrlDto(this Url urlModel)
    {
        return new RedirectUrlDto
        {
            OriginalUrl = urlModel.OriginalUrl
        };
    }

    public static UpdateUrlDto ToUpdateUrlDto(this Url urlModel)
    {
        return new UpdateUrlDto
        {
            ShortenedUrl = urlModel.ShortenedUrl,
            OriginalUrl = urlModel.OriginalUrl,
            CreatedAt = urlModel.CreatedAt
        };
    }

    public static UrlResponseDto ToUrlResponseDto(this Url urlModel)
    {
        return new UrlResponseDto
        {
            Id = urlModel.Id,
            ShortenedUrl = urlModel.ShortenedUrl,
            OriginalUrl = urlModel.OriginalUrl,
            CreatedAt = urlModel.CreatedAt
        };
    }
}
