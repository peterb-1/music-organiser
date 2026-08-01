using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Song> Songs { get; set; }
    public DbSet<UserSong> UserSongs { get; set; }
    public DbSet<UserAttribute> Attributes { get; set; }
    public DbSet<SongAttributeValue> SongAttributeValues { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Song>().HasKey(song => song.SpotifyId);
        modelBuilder.Entity<UserSong>().HasKey(userSong => new { userSong.SpotifyId, userSong.UserId });
        modelBuilder.Entity<SongAttributeValue>().HasKey(attributeValue => new { attributeValue.UserId, attributeValue.SpotifyId, attributeValue.UserAttributeId });
    }
}
