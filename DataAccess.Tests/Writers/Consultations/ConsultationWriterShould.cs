﻿using AutoFixture;
using DataAccess.DbAccess;
using DataAccess.Models;
using DataAccess.Readers.Consultations;
using DataAccess.Writers.Consultations;
using System.Threading.Tasks;
using Xunit;

namespace DataAccess.Tests.Writers.Consultations
{
    public class ConsultationWriterShould
    {
        private readonly IWriteConsultation _consultationWriter;
        private readonly IReadConsultation _consultationReader;
        private readonly Fixture _fixture;
        private readonly IPostgresqlConnection _configuration;

        public ConsultationWriterShould()
        {
            _configuration = TestHelper.GetConnection();
            _consultationWriter = new ConsultationWriter(_configuration);
            _consultationReader = new ConsultationReader(_configuration);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task AddConsultation()
        {
            //Arrange
            var sut = _fixture.Create<Consultation>();
            //Act 
            await _consultationWriter.AddConsultation(sut);
            //Assert
            var result = await _consultationReader.GetConsultationById(sut.Consultation_id);
            Assert.Equal(sut.Consultation_id, result.Consultation_id);
        }
        [Fact]
        public async Task UpdateConsultation()
        {
            //Arrange
            var sut = _fixture.Create<Consultation>();
            await _consultationWriter.AddConsultation(sut);
            //Act
            sut.Consultation_type = "updated";
            await _consultationWriter.UpdateConsultation(sut);
            //Assert
            var result = await _consultationReader.GetConsultationById(sut.Consultation_id);
            Assert.Equal(sut.Consultation_type, result.Consultation_type);
        }

        [Fact]
        public async Task DeleteConsultation()
        {
            //Arrange
            var sut = _fixture.Create<Consultation>();
            await _consultationWriter.AddConsultation(sut);
            //Act
            await _consultationWriter.DeleteConsultation(sut.Consultation_id);
            //Assert
            var result = await _consultationReader.GetConsultationById(sut.Consultation_id);
            Assert.Null(result);
        }
    }
}
