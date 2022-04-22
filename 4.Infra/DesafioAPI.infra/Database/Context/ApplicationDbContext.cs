using DesafioAPI.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DesafioAPI.infra.Database.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Starter> Starters { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}