using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace urlshortener.Models;

public class JwtSettings
{
    public String Issuer { get; set; } = null!;
    public String Audience { get; set; } = null!;
    public String Key { get; set; } = null!;
}
