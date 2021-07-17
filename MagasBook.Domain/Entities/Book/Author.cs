using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagasBook.Domain.Entities.Book
{
    [Table(nameof(Author))]
    public class Author : IBaseDomain
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        
        public List<Book> Books { get; set; }
    }
}