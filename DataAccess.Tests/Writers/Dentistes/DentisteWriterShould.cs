using AutoFixture;
using DataAccess.Models;
using DataAccess.Readers.Dentists;
using DataAccess.Writers.Dentistes;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace DataAccess.Tests.Writers.Dentistes
{
    public class DentisteWriterShould : IClassFixture<TestFixture>
    {
        private readonly Fixture _fixture;
        private ServiceProvider _serviceProvider;
        public DentisteWriterShould(TestFixture testFixture)
        {
            _serviceProvider = testFixture.ServiceProvider;
            _fixture = new Fixture();
        }

        [Fact]
        public async Task AddDentiste()
        {
            //Arrange
            var _dentisteReader = _serviceProvider.GetService<IReadDentiste>();
            var _dentisteWriter = _serviceProvider.GetService<IWriteDentiste>();
            var dentiste = _fixture.Create<Dentiste>();
            await _dentisteWriter.AddDentiste(dentiste);
            //Act
            var result = await _dentisteReader.GetDentisteById(dentiste.Dentiste_id);
            //Assert
            Assert.Equal(dentiste.Dentiste_id, result.Dentiste_id);
        }
        [Fact]
        public async Task UpdateDentiste()
        {
            //Arrange
            var _dentisteReader = _serviceProvider.GetService<IReadDentiste>();
            var _dentisteWriter = _serviceProvider.GetService<IWriteDentiste>();
            var dentiste = _fixture.Create<Dentiste>();
            await _dentisteWriter.AddDentiste(dentiste);
            var newDentiste = _fixture.Build<Dentiste>()
                .With(x => x.Dentiste_id, dentiste.Dentiste_id)
                .Create();
            //Act
            await _dentisteWriter.UpdateDentiste(newDentiste);
            var result = await _dentisteReader.GetDentisteById(dentiste.Dentiste_id);
            //Assert
            Assert.Equal(newDentiste.Dentiste_id, result.Dentiste_id);
        }

        [Fact]
        public async Task DeleteDentiste()
        {
            //Arrange
            var _dentisteReader = _serviceProvider.GetService<IReadDentiste>();
            var _dentisteWriter = _serviceProvider.GetService<IWriteDentiste>();
            var dentiste = _fixture.Create<Dentiste>();
            await _dentisteWriter.AddDentiste(dentiste);
            //Act
            await _dentisteWriter.DeleteDentiste(dentiste.Dentiste_id);
            var result = await _dentisteReader.GetDentisteById(dentiste.Dentiste_id);
            //Assert
            Assert.Null(result);
        }

    }
}
