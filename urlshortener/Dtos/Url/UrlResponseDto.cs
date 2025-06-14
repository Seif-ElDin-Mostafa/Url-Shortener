using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace urlshortener.Dtos.Url;

public class UrlResponseDto
{
    public int Id { get; set; }
    public string ShortenedUrl { get; set; } = null!;
    public string OriginalUrl { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}