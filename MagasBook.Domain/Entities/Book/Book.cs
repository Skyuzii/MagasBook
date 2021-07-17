using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagasBook.Domain.Entities.Book
{
    [Table(nameof(Book))]
    public class Book : IBaseDomain
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public int AuthorId { get; set; }
        
        public Author Author { get; set; }

        public int PublicationYear { get; set; }

        public string Description { get; set; }

        public List<Genre> Genres { get; set; }
    }
}