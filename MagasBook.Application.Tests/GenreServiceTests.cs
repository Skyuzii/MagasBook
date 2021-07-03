using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using MagasBook.Application.Dto.Book;
using MagasBook.Application.Exceptions;
using MagasBook.Application.Interfaces;
using MagasBook.Application.Mappings;
using MagasBook.Application.Services;
using MagasBook.Domain.Entities.Book;
using MagasBook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace MagasBook.Application.Tests
{
    public class GenreServiceTests
    {
        private readonly IMapper _mapper;
        private readonly GenreService _genreService;
        private readonly IApplicationDbContext _context;
        
        public GenreServiceTests()
        {
            var services = new ServiceCollection();

            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddDbContext<ApplicationDbContext>(x => x.UseInMemoryDatabase(nameof(GenreService)));
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            var provider = services.BuildServiceProvider();
            
            _mapper = provider.GetService<IMapper>();
            _context = provider.GetService<IApplicationDbContext>();
            
            _genreService = new GenreService(_context, _mapper);

            Seed();
        }

        [Fact]
        public async Task Create_ShouldCreateGenre()
        {
            // arrange
            var fixture = new Fixture();
            var genreDto = fixture.Build<GenreDto>()
                .With(x => x.Name, "Манга")
                .Create();
            
            // act
            var result = await _genreService.CreateAsync(genreDto);

            // assert
            Assert.True(result.Id > 0);
        }

        [Fact]
        public async Task Get_ShouldReturnGenre()
        {
            // arrange 
            var genreId = 1;
            
            // act
            var genre = await _genreService.GetAsync(genreId);

            // assert
            Assert.NotNull(genre);
            Assert.IsType<GenreDto>(genre);
        }

        [Fact]
        public async Task Get_GenreNotFound_ShouldThrowNotFoundException()
        {
            // arrange
            var genreId = 0;

            // act - assert
            await Assert.ThrowsAsync<NotFoundException>(() => _genreService.GetAsync(genreId));
        }

        [Fact]
        public async Task GetAll_ShouldReturnGenreDtos()
        {
            // arrange
            
            // act 
            var genreDtos = await _genreService.GetAllAsync();

            // assert
            Assert.IsType<List<GenreDto>>(genreDtos);
            Assert.True(genreDtos.Count > 0);
        }

        [Fact]
        public async Task Delete_ShouldDeleteGenre()
        {
            // arrange
            var genreId = 1;

            // act
            await _genreService.DeleteAsync(genreId);

            // assert
            Assert.False(_context.Genres.Any(x => x.Id == genreId));
        }

        [Fact]
        public async Task Delete_GenreNotFound_ShouldThrowNotFoundException()
        {
            // arrange 
            var genreId = 0;

            // act - assert
            await Assert.ThrowsAsync<NotFoundException>(() => _genreService.DeleteAsync(genreId));
        }

        private void Seed()
        {
            if (_context.Genres.Any()) return;

            var genres = new List<Genre>
            {
                new Genre {Name = "Фэнтези"},
                new Genre {Name = "Фантастика"},
                new Genre {Name = "Приключение"},
                new Genre {Name = "Боевик"}
            };

            _context.Genres.AddRange(genres);
            _context.SaveChanges();
        }
    }
}