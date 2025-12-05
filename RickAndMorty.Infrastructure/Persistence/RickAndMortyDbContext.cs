using Microsoft.EntityFrameworkCore;
using RickAndMorty.Infrastructure.Persistence.Configurations;
using RickAndMorty.Infrastructure.Persistence.Entities;

namespace RickAndMorty.Infrastructure.Persistence;

public class RickAndMortyDbContext : DbContext
{
    public RickAndMortyDbContext(DbContextOptions<RickAndMortyDbContext> options)
             : base(options) { }

    public DbSet<Character> Characters => Set<Character>();
    public DbSet<CharacterLocation> CharacterLocations => Set<CharacterLocation>();
    public DbSet<CharacterEpisode> CharacterEpisodes => Set<CharacterEpisode>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RickAndMortyDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new CharacterConfiguration());
        modelBuilder.ApplyConfiguration(new CharacterEpisodeConfiguration());
        modelBuilder.ApplyConfiguration(new CharacterLocationConfiguration());
    }
}