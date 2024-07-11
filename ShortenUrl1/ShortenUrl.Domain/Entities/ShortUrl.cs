using Microsoft.AspNetCore.Identity;
using ShortenUrl.Domain.Auth;

namespace ShortenUrl.Domain.Entities
{
    public class ShortUrl : BaseEntity
    {
        public string OriginalUrl { get; set; }
        public string ShortenUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedByUserId { get; set; }
        public IdentityUser CreatedByUser { get; set; }
        public string Description { get; set; }
    }
}