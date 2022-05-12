using AutoFixture;
using DataAccess.Models;
using DataAccess.Readers.Clients;
using DataAccess.Writers.Clients;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace DataAccess.Tests.Writers.Clients
{
    public class ClientWriterShould : IClassFixture<TestFixture>
    {
        private ServiceProvider _serviceProvider;
        private readonly Fixture _fixture;
        public ClientWriterShould(TestFixture testFixture)
        {
            _serviceProvider = testFixture.ServiceProvider;          
            _fixture = new Fixture();
        }

        [Fact]
        public async Task AddClient()
        {
            //Arrange
            var _clientReader = _serviceProvider.GetService<IReadClient>();
            var _clientWriter = _serviceProvider.GetService<IWriteClient>();
            var sut = _fixture.Create<Client>();
            //Act 
            await _clientWriter.AddClient(sut);
            //Assert
            var result = _clientReader.GetClientById(sut.Client_id);
            var resultToBeTested = result.Result;
            Assert.NotNull(resultToBeTested);
        }

        [Fact]
        public async Task UpdateClient()
        {
            //Arrange
            var _clientReader = _serviceProvider.GetService<IReadClient>();
            var _clientWriter = _serviceProvider.GetService<IWriteClient>();
            var sut = _fixture.Create<Client>();
            await _clientWriter.AddClient(sut);
            //Act 
            sut.Nom = "Updated";
            await _clientWriter.UpdateClient(sut);
            //Assert
            var result = _clientReader.GetClientById(sut.Client_id);
            var resultToBeTested = result.Result;
            Assert.Equal(sut.Nom,resultToBeTested.Nom);
        }

        [Fact]
        public async Task DeleteClient()
        {
            //Arrange
            var _clientReader = _serviceProvider.GetService<IReadClient>();
            var _clientWriter = _serviceProvider.GetService<IWriteClient>();
            var sut = _fixture.Create<Client>();
            await _clientWriter.AddClient(sut);
            //Act 
            await _clientWriter.DeleteClient(sut.Client_id);
            //Assert
            var result = _clientReader.GetClientById(sut.Client_id);
            var resultToBeTested = result.Result;
            Assert.Null(resultToBeTested);
        }
    }
}
