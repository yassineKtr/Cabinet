using AutoFixture;
using DataAccess.Models;
using DataAccess.Readers.RendezVouss;
using DataAccess.Writers.Clients;
using DataAccess.Writers.Consultations;
using DataAccess.Writers.Dentistes;
using DataAccess.Writers.RendezVouss;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace DataAccess.Tests.Writers.RendezVouss
{
    public class RendezVousWriterShould : IClassFixture<TestFixture>
    {
        private readonly Fixture _fixture;
        private ServiceProvider _serviceProvider;

        public RendezVousWriterShould(TestFixture testFixture)
        {
            _serviceProvider = testFixture.ServiceProvider;
            _fixture = new Fixture();
        }

        [Fact]
        public async Task AddRendezVous()
        {
            // Arrange
            var _clientWriter = _serviceProvider.GetService<IWriteClient>();
            var _dentisteWriter = _serviceProvider.GetService<IWriteDentiste>();
            var _consultationWriter = _serviceProvider.GetService<IWriteConsultation>();
            var _rendezVousWriter = _serviceProvider.GetService<IWriteRendezVous>();
            var _rendezVousReader = _serviceProvider.GetService<IReadRendezVous>();
            var client = _fixture.Create<Client>();
            await _clientWriter.AddClient(client);
            var dentiste = _fixture.Create<Dentiste>();
            await _dentisteWriter.AddDentiste(dentiste);
            var consultation = _fixture.Create<Consultation>();
            await _consultationWriter.AddConsultation(consultation);
            var rendezVous = _fixture.Build<RendezVous>()
                .With(x => x.Client_id, client.Client_id)
                .With(x => x.Dentiste_id, dentiste.Dentiste_id)
                .With(x => x.Consultation_id, consultation.Consultation_id)
                .Create();
            await _rendezVousWriter.AddRendezVous(rendezVous);
            //Act
            var result = await _rendezVousReader.GetRendezVousById(rendezVous.Rdv_id);
            //Assert
            Assert.Equal(rendezVous.Rdv_id, result.Rdv_id);
        }
        [Fact]
        public async Task UpdateRendezVous()
        {
            //Arrange
            var _clientWriter = _serviceProvider.GetService<IWriteClient>();
            var _dentisteWriter = _serviceProvider.GetService<IWriteDentiste>();
            var _consultationWriter = _serviceProvider.GetService<IWriteConsultation>();
            var _rendezVousWriter = _serviceProvider.GetService<IWriteRendezVous>();
            var _rendezVousReader = _serviceProvider.GetService<IReadRendezVous>();            
            var client = _fixture.Create<Client>();
            await _clientWriter.AddClient(client);
            var dentiste = _fixture.Create<Dentiste>();
            await _dentisteWriter.AddDentiste(dentiste);
            var consultation = _fixture.Create<Consultation>();
            await _consultationWriter.AddConsultation(consultation);
            var rendezVous = _fixture.Build<RendezVous>()
                .With(x => x.Client_id, client.Client_id)
                .With(x => x.Dentiste_id, dentiste.Dentiste_id)
                .With(x => x.Consultation_id, consultation.Consultation_id)
                .Create();
            await _rendezVousWriter.AddRendezVous(rendezVous);
            //Act
            var result = await _rendezVousReader.GetRendezVousById(rendezVous.Rdv_id);
            result.Date_rdv = new DateTime(2022, 1, 1);
            await _rendezVousWriter.UpdateRendezVous(result);
            var result2 = await _rendezVousReader.GetRendezVousById(rendezVous.Rdv_id);
            //Assert
            Assert.Equal(result.Rdv_id, result2.Rdv_id);
        }

        [Fact]
        public async Task DeleteRendezVous()
        {
            //Arrange
            var _clientWriter = _serviceProvider.GetService<IWriteClient>();
            var _dentisteWriter = _serviceProvider.GetService<IWriteDentiste>();
            var _consultationWriter = _serviceProvider.GetService<IWriteConsultation>();
            var _rendezVousWriter = _serviceProvider.GetService<IWriteRendezVous>();
            var _rendezVousReader = _serviceProvider.GetService<IReadRendezVous>();            
            var client = _fixture.Create<Client>();
            await _clientWriter.AddClient(client);
            var dentiste = _fixture.Create<Dentiste>();
            await _dentisteWriter.AddDentiste(dentiste);
            var consultation = _fixture.Create<Consultation>();
            await _consultationWriter.AddConsultation(consultation);
            var rendezVous = _fixture.Build<RendezVous>()
                .With(x => x.Client_id, client.Client_id)
                .With(x => x.Dentiste_id, dentiste.Dentiste_id)
                .With(x => x.Consultation_id, consultation.Consultation_id)
                .Create();
            await _rendezVousWriter.AddRendezVous(rendezVous);
            //Act
            await _rendezVousWriter.DeleteRendezVous(rendezVous.Rdv_id);
            var result = await _rendezVousReader.GetRendezVousById(rendezVous.Rdv_id);
            //Assert
            Assert.Null(result);

        }
    }
}
