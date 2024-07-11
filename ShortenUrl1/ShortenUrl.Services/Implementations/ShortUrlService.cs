using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ShortenUrl.Domain.Entities;
using ShortenUrl.Domain.Enum;
using ShortenUrl.Infrastructure.Dtos;
using ShortenUrl.Infrastructure.Exceptions;
using ShortenUrl.Persistence.Repository.Contracts;
using ShortenUrl.Services.Contracts;

namespace ShortenUrl.Services.Implementations;

public class ShortUrlService : IShortUrlService
{
    private const string AllowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<IdentityUser> _userManager;

    public ShortUrlService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<IdentityUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
    }
    
    public async Task<IEnumerable<ShortUrlDto>> GetAllShortUrls()
    {
        var shortUrls = await _unitOfWork.ShortUrlsRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ShortUrlDto>>(shortUrls);
    }

    public async Task<ShortUrlDto> GetShortUrlById(int id)
    {
        var shortUrl = await _unitOfWork.ShortUrlsRepository.GetByIdAsync(id);
        return _mapper.Map<ShortUrlDto>(shortUrl);
    }

    public async Task<ShortUrlDto> CreateShortUrl(CreateShortUrlDto createShortUrlDto)
    {
        var existingShortUrl = await _unitOfWork.ShortUrlsRepository.GetShortUrlByOriginUrlAsync(createShortUrlDto.OriginalUrl);

        if (existingShortUrl != null)
        {
            throw new DuplicateShortUrlException("URL already exists.");
        }

        var shortUrlDomain = new ShortUrl()
        {
            OriginalUrl = createShortUrlDto.OriginalUrl,
            ShortenUrl = GenerateShortUrl(createShortUrlDto.OriginalUrl),
            CreatedByUserId = createShortUrlDto.CreatedByUserId.ToString(),
            Description = createShortUrlDto.Description,
            CreatedDate = DateTime.UtcNow
        };

        await _unitOfWork.ShortUrlsRepository.AddAsync(shortUrlDomain);

        return _mapper.Map<ShortUrlDto>(shortUrlDomain);
    }

    public async Task<ShortUrlDto> UpdateShortUrl(UpdateShortUrlDto updateShortUrlDto)
    {
        var shortUrlDomain = _mapper.Map<ShortUrl>(updateShortUrlDto);
        
        var result = await _unitOfWork.ShortUrlsRepository.UpdateAsync(shortUrlDomain);

        if (result == false)
        {
            throw new NullShortUrlException("URL does not exist.");
        }
        
        return _mapper.Map<ShortUrlDto>(shortUrlDomain);
    }

    public async Task DeleteShortUrl(int id, int userId)
    {
        var shortUrl = await _unitOfWork.ShortUrlsRepository.GetByIdAsync(id);
        
        if (shortUrl == null)
            throw new InvalidDataException("Invalid params.");
        
        var identityUser = await _userManager.FindByIdAsync(userId.ToString());

        if (identityUser != null)
        {
            var roles = await _userManager.GetRolesAsync(identityUser);

            if (roles.Contains(Roles.Admin.ToString()) || identityUser.Id == shortUrl.CreatedByUserId)
            {
                await _unitOfWork.ShortUrlsRepository.RemoveByIdAsync(id);
            }
            else
            {
                throw new AccessViolationException("You are not allowed to delete this URL.");
            }
        }
    }

    private string GenerateShortUrl(string originalUrl)
    {
        Uri uri = new Uri(originalUrl);
        string baseUri = $"{uri.Scheme}://{uri.Host}";
        string shortUrlCode = GenerateShortCode();
        return $"{baseUri}/{shortUrlCode}";
    }
    
    private string GenerateShortCode()
    {
        Random random = new Random();
        string shortCode = new string(Enumerable.Repeat(AllowedCharacters, 8)
            .Select(s => s[random.Next(s.Length)]).ToArray());

        return shortCode;
    }
}