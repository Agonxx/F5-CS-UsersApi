using Microsoft.EntityFrameworkCore;
using UsersApi.Domain.Entities;
using UsersApi.Infrastructure.Data.ContextConfig;

namespace UsersApi.Infrastructure.Data
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ConfigUsuario());
        }
    }
}
