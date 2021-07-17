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
    public class AuthorService : IAuthorService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Author> _authorRepository;

        public AuthorService(IRepository<Author> authorRepository, IMapper mapper)
        {
            _mapper = mapper;
            _authorRepository = authorRepository;
        }

        public async Task DeleteAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
            {
                throw new NotFoundException();
            }

            await _authorRepository.DeleteAsync(author);
        }

        public async Task<AuthorDto> GetAsync(int id)
        {
            if (id == 0)
            {
                throw new NotFoundException();
            }

            var author = await _authorRepository.GetByIdAsync(id);
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

            await _authorRepository.AddAsync(author);

            authorDto.Id = author.Id;
            
            return authorDto;
        }

        public async Task EditAsync(AuthorDto authorDto)
        {
            var author = await _authorRepository.GetByIdAsync(authorDto.Id);
            if (author == null)
            {
                throw new NotFoundException();
            }

            author = _mapper.Map<Author>(authorDto);
            await _authorRepository.UpdateAsync(author);
        }

        public async Task<List<AuthorDto>> GetAllAsync()
        {
            var authors = await _authorRepository.GetAllAsync();
            var authorsDto = _mapper.Map<List<AuthorDto>>(authors);

            return authorsDto;
        }
    }
}