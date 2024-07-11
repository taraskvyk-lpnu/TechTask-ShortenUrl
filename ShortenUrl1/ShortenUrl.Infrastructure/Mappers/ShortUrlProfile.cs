using AutoMapper;
using ShortenUrl.Domain.Entities;
using ShortenUrl.Infrastructure.Dtos;

namespace ShortenUrl.Infrastructure.Mappers;

public class ShortUrlProfile : Profile
{
    public ShortUrlProfile()
    {
        CreateMap<ShortUrl, ShortUrlDto>().ReverseMap();
        CreateMap<CreateShortUrlDto, ShortUrl>().ReverseMap();
        CreateMap<UpdateShortUrlDto, ShortUrl>().ReverseMap();
    }
}