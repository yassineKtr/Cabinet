using AutoFixture;
using DataAccess.Models;
using DataAccess.Readers.Consultations;
using DataAccess.Writers.Consultations;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace DataAccess.Tests.Readers.Consultations
{
    public class ConsultationReaderShould : IClassFixture<TestFixture>
    {
        private readonly IReadConsultation _consultationReader;
        private readonly IWriteConsultation _consultationWriter;
        private readonly Fixture _fixture;
        private ServiceProvider _serviceProvider;

        public ConsultationReaderShould(TestFixture testFixture)
        {
            _serviceProvider = testFixture.ServiceProvider;
            _consultationReader = _serviceProvider.GetService<IReadConsultation>();
            _consultationWriter = _serviceProvider.GetService<IWriteConsultation>();
            _fixture = new Fixture();
        }

        [Fact]
        public async Task ReturnConsultation()
        {
            //Arrange
            var sut = _fixture.Create<Consultation>();
            await _consultationWriter.AddConsultation(sut);
            //Act
            var result = await _consultationReader.GetConsultationById(sut.Consultation_id);
            //Assert
            Assert.Equal(sut.Consultation_id, result.Consultation_id);
        }
        [Fact]
        public async Task ReturnNullWhenConsultationDoesNotExist()
        {
            //Arrange
            var sut = _fixture.Create<Consultation>();
            //Act
            var result = await _consultationReader.GetConsultationById(sut.Consultation_id);
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ReturnConsultationWhenGivenType()
        {
            //Arrange
            var type = _fixture.Create<string>();
            var sut = _fixture.Build<Consultation>()
                .With(x => x.Consultation_type, type)
                .Create();
            await _consultationWriter.AddConsultation(sut);
            //Act
            var result = await _consultationReader.GetConsultationByType(type);
            //Assert
            Assert.Equal(sut.Consultation_id, result.Consultation_id);
        }
    }
}
