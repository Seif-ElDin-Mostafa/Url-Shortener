using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace urlshortener.Dtos.Url;

public class CreateUrlDto
{
    public string OriginalUrl { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int UserId { get; set; }
}
