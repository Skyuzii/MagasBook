using AutoMapper;
using MagasBook.Application.Mappings;
using MagasBook.Domain.Entities.Book;

namespace MagasBook.Application.Dto.Book
{
    public class AuthorDto : IMapFrom<Author>
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Author, AuthorDto>();
            profile.CreateMap<Author, AuthorDto>().ReverseMap();
        }
    }
}