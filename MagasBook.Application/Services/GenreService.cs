using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MagasBook.Application.Dto.Book;
using MagasBook.Application.Exceptions;
using MagasBook.Application.Interfaces;
using MagasBook.Domain.Entities.Book;
using Microsoft.EntityFrameworkCore;

namespace MagasBook.Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GenreService(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task DeleteAsync(int id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == id);
            if (genre == null)
            {
                throw new NotFoundException();
            }

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
        }

        public async Task<GenreDto> GetAsync(int id)
        {
            if (id == 0)
            {
                throw new NotFoundException();
            }

            var genre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == id);
            if (genre == null)
            {
                throw new NotFoundException();
            }
            
            var genreDto = _mapper.Map<GenreDto>(genre);

            return genreDto;
        }

        public async Task<GenreDto> CreateAsync(GenreDto genreDto)
        {
            var genre = _mapper.Map<Genre>(genreDto);

            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();

            genreDto.Id = genre.Id;
            
            return genreDto;
        }

        public async Task<List<GenreDto>> GetAllAsync()
        {
            var genres = await _context.Genres.ToListAsync();
            var genresDto = _mapper.Map<List<GenreDto>>(genres);

            return genresDto;
        }
    }
}