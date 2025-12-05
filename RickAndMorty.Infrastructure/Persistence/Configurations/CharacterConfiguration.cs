using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RickAndMorty.Infrastructure.Persistence.Entities;

namespace RickAndMorty.Infrastructure.Persistence.Configurations;

internal class CharacterConfiguration : IEntityTypeConfiguration<Character>
{
    public void Configure(EntityTypeBuilder<Character> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Status).HasMaxLength(50);
        builder.Property(c => c.Species).HasMaxLength(50);
        builder.Property(c => c.Type).HasMaxLength(50);
        builder.Property(c => c.Gender).HasMaxLength(50);
        builder.Property(c => c.Image).HasMaxLength(500);
        builder.Property(c => c.Url).HasMaxLength(500);
        builder.Property(c => c.Created).IsRequired();

        builder.HasOne(c => c.Origin)
            .WithMany()
            .HasForeignKey(c => c.OriginId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Location)
            .WithMany()
            .HasForeignKey(c => c.LocationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Episodes)
            .WithOne((ce => ce.Character))
            .HasForeignKey(e => e.CharacterId);
    }
}