using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using urlshortener.Data;
using urlshortener.Dtos.Url;
using urlshortener.Interfaces;
using urlshortener.Mappers;

namespace urlshortener.Controllers;

[Route("api/url")]
[ApiController]
public class UrlController : ControllerBase
{
    private readonly IUrlRepository _urlRepository;
    public UrlController(IUrlRepository urlRepository)
    {
        _urlRepository = urlRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var urls = await _urlRepository.GetAllAsync();
        return Ok(urls);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] int Id)
    {
        var url = await _urlRepository.GetByIdAsync(Id);
        if (url == null) return NotFound();
        return Ok(url.ToUrlResponseDto());
    }

    [HttpGet("user/{UserId}")]
    public async Task<IActionResult> GetByUserId([FromRoute] int UserId)
    {
        var urls = await _urlRepository.GetByUserIdAsync(UserId);
        if (urls == null || !urls.Any()) return NotFound("No URLs found for this user.");
        return Ok(urls);
    }

    [HttpGet("redirect/{ShortenedUrl}")]
    public async Task<IActionResult> RedirectUrl([FromRoute] string ShortenedUrl)
    {
        var url = await _urlRepository.GetByShortenedUrlAsync(ShortenedUrl);
        if (url == null) return NotFound();

        return Redirect(url.ToRedirectUrlDto().OriginalUrl);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUrl([FromBody] CreateUrlDto createUrlDto)
    {
        using var transaction = await _urlRepository.CreateTransactionAsync();

        try
        {
            var existingUrl = await _urlRepository.GetByOriginalUrlAsync(createUrlDto.OriginalUrl);
            if (existingUrl != null) return BadRequest("Url already exists");

            var url = createUrlDto.ToCreateUrl();
            await _urlRepository.AddAsync(url);

            url.ShortenedUrl = Utility.HashUrl.GenerateShortUrl(createUrlDto.OriginalUrl, url.UserId);
            await _urlRepository.SaveChangesAsync();

            await transaction.CommitAsync();

            return StatusCode(StatusCodes.Status201Created, url.ToUrlResponseDto());
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateUrl([FromRoute] int Id, [FromBody] UpdateUrlDto updateUrlDto)
    {
        var url = await _urlRepository.GetByIdAsync(Id);
        if (url == null) return NotFound();

        url.ShortenedUrl = updateUrlDto.ShortenedUrl;
        url.OriginalUrl = updateUrlDto.OriginalUrl;
        url.CreatedAt = updateUrlDto.CreatedAt;

        await _urlRepository.SaveChangesAsync();
        return Ok(url.ToUrlResponseDto());
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteUrl([FromRoute] int Id)
    {
        var url = await _urlRepository.GetByIdAsync(Id);
        if (url == null) return NotFound();

        _urlRepository.Remove(url);
        await _urlRepository.SaveChangesAsync();
        return NoContent();
    }
}