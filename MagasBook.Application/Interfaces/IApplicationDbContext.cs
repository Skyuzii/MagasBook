using System.Threading;
using System.Threading.Tasks;
using MagasBook.Domain.Entities.Book;
using Microsoft.EntityFrameworkCore;

namespace MagasBook.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}