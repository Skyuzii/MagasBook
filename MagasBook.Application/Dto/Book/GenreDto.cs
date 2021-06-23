using AutoMapper;
using MagasBook.Application.Mappings;

namespace MagasBook.Application.Dto.Book
{
    public class GenreDto : IMapFrom<Domain.Entities.Book.Genre>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GenreDto, Domain.Entities.Book.Genre>();
            profile.CreateMap<GenreDto, Domain.Entities.Book.Genre>().ReverseMap();
        }
    }
}