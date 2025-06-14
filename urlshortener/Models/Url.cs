using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace urlshortener.Models;

public class Url
{
    public int Id { get; set; }
    public string ShortenedUrl { get; set; } = null!;
    public string OriginalUrl { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int UserId { get; set; }
}