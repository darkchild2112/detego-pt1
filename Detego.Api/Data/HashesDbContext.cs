using Microsoft.EntityFrameworkCore;

namespace Detego.Api.Data;

public class HashesDbContext(DbContextOptions<HashesDbContext> options) : DbContext(options)
{
    public DbSet<Hash> Hashes { get; set; }
}

