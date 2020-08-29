using System;
using Ackee.Domain.Model.TestUtility;
using Microsoft.EntityFrameworkCore;

namespace Ackee.DataAccess.EfCore.IntegrationTests
{
    public sealed class EfCoreDbContext : AckeeDbContext
    {
        private string _path;

        public EfCoreDbContext():base()
        {
            Database.Migrate();
        }
        public EfCoreDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _path = Environment.CurrentDirectory + "\\book.db";
            var randomDatabaseName = $@"Data Source={_path}";
            optionsBuilder.UseSqlite(randomDatabaseName);
            
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasKey(a => a.Id);
            modelBuilder.Entity<Book>().Property(a => a.Id)
                .HasConversion(a => a.DbId,
                    a => new BookId(a));

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Book> Books { get; set; }
    }
}