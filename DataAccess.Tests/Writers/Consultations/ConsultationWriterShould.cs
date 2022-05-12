using AutoFixture;
using DataAccess.Models;
using DataAccess.Readers.Consultations;
using DataAccess.Writers.Consultations;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace DataAccess.Tests.Writers.Consultations
{
    public class ConsultationWriterShould : IClassFixture<TestFixture>
    {
        private readonly Fixture _fixture;
        private ServiceProvider _serviceProvider;
        public ConsultationWriterShould(TestFixture testFixture)
        {
            _serviceProvider = testFixture.ServiceProvider;
            _fixture = new Fixture();
        }

        [Fact]
        public async Task AddConsultation()
        {
            //Arrange
            var _consultationWriter = _serviceProvider.GetService<IWriteConsultation>();
            var _consultationReader = _serviceProvider.GetService<IReadConsultation>();
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
            var _consultationWriter = _serviceProvider.GetService<IWriteConsultation>();
            var _consultationReader = _serviceProvider.GetService<IReadConsultation>();
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
            var _consultationWriter = _serviceProvider.GetService<IWriteConsultation>();
            var _consultationReader = _serviceProvider.GetService<IReadConsultation>();            
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
