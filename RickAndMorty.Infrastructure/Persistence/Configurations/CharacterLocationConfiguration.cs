using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RickAndMorty.Infrastructure.Persistence.Entities;

namespace RickAndMorty.Infrastructure.Persistence.Configurations;

internal class CharacterLocationConfiguration : IEntityTypeConfiguration<CharacterLocation>
{
    public void Configure(EntityTypeBuilder<CharacterLocation> builder)
    {
        builder.HasKey(cl => cl.Id);

        builder.Property(cl => cl.Name)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(cl => cl.Url)
               .HasMaxLength(500);
    }
}
