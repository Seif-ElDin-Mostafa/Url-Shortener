using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using urlshortener.Data;
using urlshortener.Interfaces;
using urlshortener.Models;

namespace urlshortener.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
    }
    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Username == username);
    }
    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }
    public async Task RemoveAsync(User user)
    {
        _context.Users.Remove(user);
    }
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
