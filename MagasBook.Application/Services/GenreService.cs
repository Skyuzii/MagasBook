using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MagasBook.Application.Dto.Book;
using MagasBook.Application.Exceptions;
using MagasBook.Application.Interfaces.Repositories;
using MagasBook.Application.Interfaces.Services;
using MagasBook.Domain.Entities.Book;

namespace MagasBook.Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Genre> _genreRepository;

        public GenreService(IRepository<Genre> genreRepository, IMapper mapper)
        {
            _mapper = mapper;
            _genreRepository = genreRepository;
        }

        public async Task DeleteAsync(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            if (genre == null)
            {
                throw new NotFoundException();
            }

            await _genreRepository.DeleteAsync(genre);
        }

        public async Task<GenreDto> GetAsync(int id)
        {
            if (id == 0)
            {
                throw new NotFoundException();
            }

            var genre = await _genreRepository.GetByIdAsync(id);
            if (genre == null)
            {
                throw new NotFoundException();
            }
            
            var genreDto = _mapper.Map<GenreDto>(genre);

            return genreDto;
        }

        public async Task<GenreDto> CreateAsync(GenreDto genreDto)
        {
            var genre = await _genreRepository.AddAsync(_mapper.Map<Genre>(genreDto));
            genreDto.Id = genre.Id;
            
            return genreDto;
        }

        public async Task<List<GenreDto>> GetAllAsync()
        {
            var genres = await _genreRepository.GetAllAsync();
            var genreDtos = _mapper.Map<List<GenreDto>>(genres);

            return genreDtos;
        }
    }
}