using MagasBook.Application.Interfaces;
using MagasBook.Domain.Entities.Account;
using MagasBook.Domain.Entities.Book;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagasBook.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        
        public DbSet<Genre> Genres { get; set; }
        
        public DbSet<Author> Authors { get; set; }
    }
}