using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using urlshortener.Dtos.Url;

namespace urlshortener.Dtos.User;

public class CreateUserDto
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}