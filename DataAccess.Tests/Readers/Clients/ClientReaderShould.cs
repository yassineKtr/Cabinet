using AutoFixture;
using DataAccess.Models;
using DataAccess.Readers.Clients;
using DataAccess.Writers.Clients;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace DataAccess.Tests.Readers.Clients
{
    public class ClientReaderShould : IClassFixture<TestFixture>
    {
        private readonly IWriteClient _clientWriter;
        private readonly IReadClient _clientReader;
        private readonly Fixture _fixture;
        private ServiceProvider _serviceProvider;
        public ClientReaderShould(TestFixture testFixture)
        {
            _serviceProvider = testFixture.ServiceProvider;
            _fixture = new Fixture();
            _clientWriter = _serviceProvider.GetService<IWriteClient>();
            _clientReader = _serviceProvider.GetService<IReadClient>();
        }

        [Fact]
        public async Task ReturnClient()
        {
            //Arrange
            var sut = _fixture.Create<Client>();
            await _clientWriter.AddClient(sut);
            //Act
            var result = await _clientReader.GetClientById(sut.Client_id);
            //Assert
            Assert.Equal(sut.Client_id, result.Client_id);
        }
        [Fact]
        public async Task ReturnNull()
        {
            //Arrange
            var sut = _fixture.Create<Client>();
            //Act
            var result = await _clientReader.GetClientById(sut.Client_id);
            //Assert
            Assert.Null(result);
        }
       
    }
}
