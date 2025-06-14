using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using urlshortener.Data;
using urlshortener.Dtos.User;
using urlshortener.Interfaces;
using urlshortener.Mappers;
using urlshortener.Models;
using urlshortener.Repositories;

namespace urlshortener.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    public UserController(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
    {
        var user = registerUserDto.ToCreateUser();
        var existingEmail = await _userRepository.GetByEmailAsync(user.Email);
        if (existingEmail != null)
        {
            return BadRequest("Email already exists.");
        }

        var existingUsername = await _userRepository.GetByUsernameAsync(user.Username);
        if (existingUsername != null)
        {
            return BadRequest("Username already exists.");
        }

        var passwordHasher = new PasswordHasher<User>();
        string hashedPassword = passwordHasher.HashPassword(user, user.Password);
        user.Password = hashedPassword;

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        return StatusCode(StatusCodes.Status201Created, user.ToUserResponseDto());
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
    {
        var user = await _userRepository.GetByUsernameAsync(loginUserDto.Username);
        if (user == null)
        {
            return Unauthorized("Invalid username.");
        }

        var passwordHasher = new PasswordHasher<User>();
        var result = passwordHasher.VerifyHashedPassword(user, user.Password, loginUserDto.Password);

        if (result == PasswordVerificationResult.Failed)
        {
            return Unauthorized("Invalid password.");
        }

        var jwtSettings = _configuration.GetSection("JwtSettings").Get<JwtSettings>();
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            signingCredentials: creds,
            claims: new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("username", user.Username)
            });

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return Ok(new { token = tokenString });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return NotFound();

        await _userRepository.RemoveAsync(user);
        await _userRepository.SaveChangesAsync();
        return NoContent();
    }
}
