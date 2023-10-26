using DomainCentricDemo.Domain;
using Microsoft.EntityFrameworkCore;

namespace DomainCentricDemo.Infrastrcture {
    public class BookContext : DbContext {
        private readonly bool _designTime;

        public BookContext() {
            _designTime = true;
        }

        public BookContext(DbContextOptions<BookContext> options) : base(options) {
            //_designTime = false;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (_designTime) {
                optionsBuilder.UseSqlServer(
                    @"Server=(localdb)\\mssqllocaldb;Initial Catalog=BookDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
            }
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

    }
}
