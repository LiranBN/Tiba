using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Tiba.Domain.Models;

namespace Tiba.Domain.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Repository> Repositories { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Favorite>().HasKey(table => new {
                table.RepositoryId,
                table.UserId
            });
        }
    }
}
