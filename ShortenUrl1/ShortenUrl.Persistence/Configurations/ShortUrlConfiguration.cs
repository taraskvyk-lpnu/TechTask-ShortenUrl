using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShortenUrl.Domain.Entities;

namespace ShortenUrl.Persistence.Configurations;

public class ShortUrlConfiguration : IEntityTypeConfiguration<ShortUrl>
{
    public void Configure(EntityTypeBuilder<ShortUrl> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.OriginalUrl)
            .IsRequired()
            .HasMaxLength(2048);

        builder.Property(e => e.ShortenUrl)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(e => e.CreatedDate)
            .IsRequired();
        
        builder.Property(e => e.Description)
            .HasMaxLength(2000);
        
        builder.HasOne(e => e.CreatedByUser)
            .WithMany()
            .HasForeignKey(e => e.CreatedByUserId)
            .IsRequired();
    }
}