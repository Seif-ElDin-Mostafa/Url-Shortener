using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using urlshortener.Dtos.Url;
using urlshortener.Models;

namespace urlshortener.Interfaces;

public interface IUrlRepository : IBaseRepository
{
    Task<List<UrlResponseDto>> GetAllAsync();
    Task<Url?> GetByIdAsync(int Id);
    Task<List<UrlResponseDto>> GetByUserIdAsync(int userId);
    Task<Url?> GetByShortenedUrlAsync(string shortenedUrl);
    Task<Url?> GetByOriginalUrlAsync(string originalUrl);
    Task AddAsync(Url url);
    void Remove(Url url);
    Task SaveChangesAsync();
}