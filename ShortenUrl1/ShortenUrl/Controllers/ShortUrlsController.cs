using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShortenUrl.Infrastructure.Dtos;
using ShortenUrl.Services.Contracts;

namespace ShortenUrl1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShortUrlsController : ControllerBase
{
    private readonly IShortUrlService _shortService;

    public ShortUrlsController(IShortUrlService shortService)
    {
        _shortService = shortService;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShortUrlDto>>> GetAllShortUrls()
    {
        var shortUrls = await _shortService.GetAllShortUrls();
        return Ok(shortUrls);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ShortUrlDto>> GetShortUrlById(int id)
    {
        var shortUrl = await _shortService.GetShortUrlById(id);
        return Ok(shortUrl);
    }
    
    //[Authorize]
    [HttpPost]
    public async Task<ActionResult<ShortUrlDto>> CreateShortUrl([FromBody] CreateShortUrlDto createShortUrlDto)
    {
        var shortUrl = await _shortService.CreateShortUrl(createShortUrlDto);
        return CreatedAtAction(nameof(GetShortUrlById), new { id = shortUrl.Id }, shortUrl);
    }
    
    //[Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<ShortUrlDto>> UpdateShortUrl(int id, UpdateShortUrlDto updateShortUrlDto)
    {
        if (id != updateShortUrlDto.Id)
        {
            return BadRequest();
        }

        var shortUrl = await _shortService.UpdateShortUrl(updateShortUrlDto);
        return Ok(shortUrl);
    }
    
    //[Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteShortUrl(int id, [FromQuery] int userId)
    {
        await _shortService.DeleteShortUrl(id, userId);
        return Ok(id);
    }
}