using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace urlshortener.Dtos.User;

public class LoginUserDto
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}
