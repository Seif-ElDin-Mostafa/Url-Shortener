using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using urlshortener.Dtos.User;
using urlshortener.Models;

namespace urlshortener.Mappers;

public static class UserMappers
{
    public static User ToCreateUser(this CreateUserDto createUserDto)
    {
        return new User
        {
            Username = createUserDto.Username,
            Email = createUserDto.Email,
            Password = createUserDto.Password,
        };
    }

    public static UserResponseDto ToUserResponseDto(this User userModel)
    {
        return new UserResponseDto
        {
            Id = userModel.Id,
            Username = userModel.Username,
            Email = userModel.Email,
        };
    }
}
