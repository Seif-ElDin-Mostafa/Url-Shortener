using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using urlshortener.Dtos.User;
using urlshortener.Models;

namespace urlshortener.Mappers;

public static class UserMappers
{
    public static User ToLoginUser(this LoginUserDto loginUserDto)
    {
        return new User
        {
            Username = loginUserDto.Username,
            Password = loginUserDto.Password,
        };
    }
    public static User ToCreateUser(this RegisterUserDto registerUserDto)
    {
        return new User
        {
            Username = registerUserDto.Username,
            Email = registerUserDto.Email,
            Password = registerUserDto.Password,
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
