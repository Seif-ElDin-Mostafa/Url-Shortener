using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using urlshortener.Data;
using urlshortener.Dtos.Url;
using urlshortener.Interfaces;
using urlshortener.Mappers;
using urlshortener.Models;

namespace urlshortener.Repositories;

public class UrlRepository : IUrlRepository
{
    private readonly ApplicationDbContext _context;
    public UrlRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<UrlResponseDto>> GetAllAsync()
    {
        return await _context.Urls.AsNoTracking().Select(u => u.ToUrlResponseDto()).ToListAsync();
    }
    public async Task<Url?> GetByIdAsync(int Id)
    {
        return await _context.Urls.FirstOrDefaultAsync(u => u.Id == Id);
    }
    public async Task<List<UrlResponseDto>> GetByUserIdAsync(int userId)
    {
        return await _context.Urls
            .AsNoTracking()
            .Where(u => u.UserId == userId)
            .Select(u => u.ToUrlResponseDto())
            .ToListAsync();
    }
    public async Task<Url?> GetByShortenedUrlAsync(string shortenedUrl)
    {
        return await _context.Urls.AsNoTracking().FirstOrDefaultAsync(u => u.ShortenedUrl == shortenedUrl);
    }
    public async Task<Url?> GetByOriginalUrlAsync(string originalUrl)
    {
        return await _context.Urls.AsNoTracking().FirstOrDefaultAsync(u => u.OriginalUrl == originalUrl);
    }
    public async Task AddAsync(Url url)
    {
        await _context.Urls.AddAsync(url);
    }
    public void Remove(Url url)
    {
        _context.Urls.Remove(url);
    }
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
    public async Task<DbTransaction> CreateTransactionAsync()
    {
        if (_context.Database.CurrentTransaction != null)
        {
            await _context.Database.CurrentTransaction.DisposeAsync();
        }

        return (await _context.Database.BeginTransactionAsync()).GetDbTransaction();
    }
}
