using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RickAndMorty.Infrastructure.Persistence.Entities;

namespace RickAndMorty.Infrastructure.Persistence.Configurations;

internal class CharacterEpisodeConfiguration : IEntityTypeConfiguration<CharacterEpisode>
{
    public void Configure(EntityTypeBuilder<CharacterEpisode> builder)
    {
        builder.HasKey(ce => new { ce.CharacterId, ce.EpisodeUrl });

        builder.HasOne(ce => ce.Character)
        .WithMany(c => c.Episodes)
        .HasForeignKey(ce => ce.CharacterId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.Property(ce => ce.EpisodeUrl)
            .IsRequired()
            .HasMaxLength(500);
    }
}
