using AutoFixture;
using DataAccess.Models;
using DataAccess.Readers.Dentists;
using DataAccess.Writers.Dentistes;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace DataAccess.Tests.Readers.Dentistes
{
    public class DentisteReaderShould : IClassFixture<TestFixture>
    {
        private readonly IReadDentiste _dentisteReader;
        private readonly IWriteDentiste _dentisteWriter;
        private readonly Fixture _fixture;
        private ServiceProvider _serviceProvider;

        public DentisteReaderShould(TestFixture testFixture)
        {
            _serviceProvider = testFixture.ServiceProvider;
            _dentisteReader = _serviceProvider.GetService<IReadDentiste>();
            _dentisteWriter = _serviceProvider.GetService<IWriteDentiste>();
            _fixture = new Fixture();
        }

        [Fact]
        public async Task ReturnDentiste()
        {
            //Arrange
            var sut = _fixture.Create<Dentiste>();
            await _dentisteWriter.AddDentiste(sut);
            //Act
            var result = await _dentisteReader.GetDentisteById(sut.Dentiste_id);
            //Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task ReturnNull()
        {
            //Arrange
            var sut = _fixture.Create<Dentiste>();
            //Act
            var result = await _dentisteReader.GetDentisteById(sut.Dentiste_id);
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ReturnDentisteByName()
        {
            //Arrange
            var sut = _fixture.Create<Dentiste>();
            await _dentisteWriter.AddDentiste(sut);
            //Act
            var result = await _dentisteReader.GetDentisteByName(sut.Nom);
            //Assert
            Assert.NotNull(result);
        }
    }
}
