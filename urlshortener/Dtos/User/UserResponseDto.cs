using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace urlshortener.Dtos.User;

public class UserResponseDto
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
}
