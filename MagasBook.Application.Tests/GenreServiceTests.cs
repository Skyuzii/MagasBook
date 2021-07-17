using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using MagasBook.Application.Dto.Book;
using MagasBook.Application.Exceptions;
using MagasBook.Application.Interfaces.Repositories;
using MagasBook.Application.Mappings;
using MagasBook.Application.Services;
using MagasBook.Domain.Entities.Book;
using Moq;
using Xunit;

namespace MagasBook.Application.Tests
{
    public class GenreServiceTests
    {
        private readonly GenreService _genreService;
        private readonly Fixture _fixture = new Fixture();
        private readonly Mock<IRepository<Genre>> _genreRepositoryMock = new Mock<IRepository<Genre>>();
        
        public GenreServiceTests()
        {
            var mapperConfiguration = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
            _genreService = new GenreService(_genreRepositoryMock.Object, mapperConfiguration.CreateMapper());
        }

        [Fact]
        public async Task Create_ShouldCreateGenre()
        {
            // arrange
            var genreId = _fixture.Create<int>();
            var genreName = "Манга";
            var genreDto = _fixture.Build<GenreDto>()
                .With(x => x.Name, genreName)
                .Create();

            var genre = _fixture.Build<Genre>()
                .With(x => x.Id, genreId)
                .With(x => x.Name, genreName)
                .Without(x => x.Books)
                .Create();

            _genreRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<Genre>()))
                .ReturnsAsync(genre);
            
            // act
            var resultGenreDto = await _genreService.CreateAsync(genreDto);

            // assert
            _genreRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Genre>()), Times.Once);
            Assert.Equal(resultGenreDto.Id, genre.Id);
            Assert.Equal(resultGenreDto.Name, genre.Name);
        }

        [Fact]
        public async Task Get_ShouldReturnGenre()
        {
            // arrange 
            var genreId = _fixture.Create<int>();
            var genreName = "Манга";
            var genre = _fixture.Build<Genre>()
                .With(x => x.Id, genreId)
                .With(x => x.Name, genreName)
                .Without(x => x.Books)
                .Create();
            
            _genreRepositoryMock
                .Setup(x => x.GetByIdAsync(genreId))
                .ReturnsAsync(genre);
            
            // act
            var genreDto = await _genreService.GetAsync(genreId);

            // assert
            _genreRepositoryMock.Verify(x => x.GetByIdAsync(genreId), Times.Once);
            Assert.NotNull(genreDto);
            Assert.Equal(genreId, genreDto.Id);
            Assert.Equal(genreName, genreDto.Name);
        }

        [Fact]
        public async Task Get_GenreNotFound_ShouldThrowNotFoundException()
        {
            // arrange
            var genreId = _fixture.Create<int>();

            _genreRepositoryMock
                .Setup(x => x.GetByIdAsync(genreId))
                .ReturnsAsync(() => null);

            // act - assert
            await Assert.ThrowsAsync<NotFoundException>(() => _genreService.GetAsync(genreId));
            _genreRepositoryMock.Verify(x => x.GetByIdAsync(genreId), Times.Once);
        }

        [Fact]
        public async Task GetAll_ShouldReturnGenreDtos()
        {
            // arrange

            var genreCount = 5;
            var genres = _fixture.Build<Genre>()
                .Without(x => x.Books)
                .CreateMany(genreCount)
                .ToList();

            _genreRepositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(genres);
            
            // act 
            var genreDtos = await _genreService.GetAllAsync();

            // assert
            _genreRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
            Assert.NotNull(genreDtos);
            Assert.True(genreDtos.Count == genreCount);
        }

        [Fact]
        public async Task Delete_ShouldDeleteGenre()
        {
            // arrange
            var genreId = _fixture.Create<int>();
            var genre = _fixture.Build<Genre>()
                .With(x => x.Id, genreId)
                .Without(x => x.Books)
                .Create();

            _genreRepositoryMock
                .Setup(x => x.GetByIdAsync(genreId))
                .ReturnsAsync(genre);
            
            _genreRepositoryMock
                .Setup(x => x.DeleteAsync(genre))
                .Returns(Task.CompletedTask);

            // act
            await _genreService.DeleteAsync(genreId);

            // assert
            _genreRepositoryMock.Verify(x => x.DeleteAsync(genre), Times.Once);
            _genreRepositoryMock.Verify(x => x.GetByIdAsync(genreId), Times.Once);
        }

        [Fact]
        public async Task Delete_GenreNotFound_ShouldThrowNotFoundException()
        {
            // arrange
            var genreId = _fixture.Create<int>();

            _genreRepositoryMock
                .Setup(x => x.GetByIdAsync(genreId))
                .ReturnsAsync(() => null);

            // act - assert
            await Assert.ThrowsAsync<NotFoundException>(() => _genreService.DeleteAsync(genreId));
            _genreRepositoryMock.Verify(x => x.GetByIdAsync(genreId), Times.Once);
        }
    }
}