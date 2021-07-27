using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagasBook.Domain.Entities.Book
{
    [Table(nameof(Genre))]
    public class Genre : IBaseDomain
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public List<Book> Books { get; set; }
    }
}