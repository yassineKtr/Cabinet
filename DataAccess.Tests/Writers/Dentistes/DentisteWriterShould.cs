using AutoFixture;
using DataAccess.DbAccess;
using DataAccess.Models;
using DataAccess.Readers.Dentists;
using DataAccess.Writers.Dentistes;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace DataAccess.Tests.Writers.Dentistes
{
    public class DentisteWriterShould
    {
        private readonly IWriteDentiste _dentisteWriter;
        private readonly IReadDentiste _dentisteReader;
        private readonly Fixture _fixture;
        private readonly IPostgresqlConnection _configuration;

        public DentisteWriterShould()
        {
            _configuration = TestHelper.GetConnection();
            _dentisteWriter = new DentisteWriter(_configuration);
            _dentisteReader = new DentisteReader(_configuration);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task AddDentiste()
        {
            //Arrange
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
