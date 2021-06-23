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
    public class AuthorService : IAuthorService
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public AuthorService(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task DeleteAsync(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
            if (author == null)
            {
                throw new NotFoundException();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }

        public async Task<AuthorDto> GetAsync(int id)
        {
            if (id == 0)
            {
                throw new NotFoundException();
            }

            var author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
            if (author == null)
            {
                throw new NotFoundException();
            }
            
            var authorDto = _mapper.Map<AuthorDto>(author);

            return authorDto;
        }

        public async Task<AuthorDto> CreateAsync(AuthorDto authorDto)
        {
            var author = _mapper.Map<Author>(authorDto);

            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();

            authorDto.Id = author.Id;
            
            return authorDto;
        }

        public async Task EditAsync(AuthorDto authorDto)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == authorDto.Id);
            if (author == null)
            {
                throw new NotFoundException();
            }

            author = _mapper.Map<Author>(authorDto);
            await _context.SaveChangesAsync();
        }

        public async Task<List<AuthorDto>> GetAllAsync()
        {
            var authors = await _context.Authors.ToListAsync();
            var authorsDto = _mapper.Map<List<AuthorDto>>(authors);

            return authorsDto;
        }
    }
}