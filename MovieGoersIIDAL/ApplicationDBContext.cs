using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MovieGoersIIBL;
using System;
using System.IO;

namespace MovieGoersIIDAL
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options)
        {

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<UserCollection> UserCollection { get; set; }
        public DbSet<Movies> Movies { get; set; }

        public class ApplicationDesignTimeContext : IDesignTimeDbContextFactory<ApplicationDBContext>
        {
            public ApplicationDBContext CreateDbContext(string[] args)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../MovieGoersII/appsettings.json").Build();
                var builder = new DbContextOptionsBuilder<ApplicationDBContext>();
                var connectionString = configuration.GetConnectionString("DatabaseConnection");
                builder.UseSqlServer(connectionString);
                return new ApplicationDBContext(builder.Options);
            }
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //}
    }
}
