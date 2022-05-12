﻿using AutoFixture;
using DataAccess.DbAccess;
using DataAccess.Models;
using DataAccess.Readers.Clients;
using DataAccess.Writers.Clients;
using System.Threading.Tasks;
using Xunit;

namespace DataAccess.Tests.Readers.Clients
{
    public class ClientReaderShould
    {
        private readonly IWriteClient _clientWriter;
        private readonly IReadClient _clientReader;
        private readonly Fixture _fixture;
        private readonly IPostgresqlConnection _configuration;

        public ClientReaderShould()
        {
            _configuration = TestHelper.GetConnection();
            _clientReader = new ClientReader(_configuration);
            _clientWriter = new ClientWriter(_configuration);
            _fixture = new Fixture();
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
