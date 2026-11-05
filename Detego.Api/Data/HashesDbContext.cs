using Microsoft.EntityFrameworkCore;

namespace Detego.Api.Data;

public sealed class HashesDbContext(DbContextOptions<HashesDbContext> options) : DbContext(options)
{
    public DbSet<Hash> Hashes { get; set; }
}

