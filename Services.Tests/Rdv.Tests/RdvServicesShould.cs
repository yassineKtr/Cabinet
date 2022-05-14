using AutoFixture;
using DataAccess.Models;
using DataAccess.Readers.RendezVouss;
using DataAccess.Tests;
using DataAccess.Writers.Clients;
using DataAccess.Writers.Consultations;
using DataAccess.Writers.Dentistes;
using DataAccess.Writers.RendezVouss;
using Microsoft.Extensions.DependencyInjection;
using Services.Rdv;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Services.Tests.Rdv.Tests
{
    public class RdvServicesShould : IClassFixture<TestFixture>
    {
        private readonly IRdvServices _rdvServices;
        private readonly IWriteRendezVous _renderVousWriter;
        private readonly IReadRendezVous _renderVousReader;
        private readonly IWriteDentiste _dentisteWriter;
        private readonly IWriteClient _clientWriter;
        private readonly IWriteConsultation _consultationWriter;
        private ServiceProvider _serviceProvider;
        private readonly Fixture _fixture;
        public RdvServicesShould(TestFixture testFixture)
        {
            _serviceProvider = testFixture.ServiceProvider;
            _rdvServices = _serviceProvider.GetService<IRdvServices>();
            _renderVousWriter = _serviceProvider.GetService<IWriteRendezVous>();
            _renderVousReader = _serviceProvider.GetService<IReadRendezVous>();
            _dentisteWriter = _serviceProvider.GetService<IWriteDentiste>();
            _clientWriter = _serviceProvider.GetService<IWriteClient>();
            _consultationWriter = _serviceProvider.GetService<IWriteConsultation>();
            _fixture = new Fixture();
        }

        [Fact]
        public async Task CreateRdvIfNumberOfDentistesIsEnough()
        {
            //Arrange
            var dentiste = _fixture.Build<Dentiste>()
                .With(x => x.Max_clients, 2)
                .Create();
            var client = _fixture.Create<Client>();
            var consultation = _fixture.Create<Consultation>();
            var date = _fixture.Create<DateTime>();
            var rdv = _fixture.Build<RendezVous>()
                .With(x => x.Consultation_id, consultation.Consultation_id)
                .With(x => x.Client_id, client.Client_id)
                .With(x => x.Dentiste_id, dentiste.Dentiste_id)
                .Create();
            await _dentisteWriter.AddDentiste(dentiste);
            await _clientWriter.AddClient(client);
            await _consultationWriter.AddConsultation(consultation);
            await _renderVousWriter.AddRendezVous(rdv);
            //Act 
            await _rdvServices.CreateRdv(dentiste.Nom, client.Client_id, consultation.Consultation_type, date);
            var result = await _renderVousReader.GetRendezVousByDentisteId(dentiste.Dentiste_id);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ReturnErrorIfNumberOfDentistesIsSurpassed()
        {
            //Arrange
            var dentiste = _fixture.Build<Dentiste>()
                .With(x => x.Max_clients, 1)
                .Create();
            var client = _fixture.Create<Client>();
            var consultation = _fixture.Create<Consultation>();
            var date = _fixture.Create<DateTime>();
            var rdv = _fixture.Build<RendezVous>()
                .With(x => x.Consultation_id, consultation.Consultation_id)
                .With(x => x.Client_id, client.Client_id)
                .With(x => x.Dentiste_id, dentiste.Dentiste_id)
                .Create();
            await _dentisteWriter.AddDentiste(dentiste);
            await _clientWriter.AddClient(client);
            await _consultationWriter.AddConsultation(consultation);
            await _renderVousWriter.AddRendezVous(rdv);
            //Act 
            
            //Assert
            await Assert.ThrowsAsync<Exception>(async () => await _rdvServices.CreateRdv(dentiste.Nom, client.Client_id, consultation.Consultation_type, date));
        }

        [Fact]
        public async Task CancelRdvIfExists()
        {
            //Arrange
            var dentiste = _fixture.Create<Dentiste>();
            var client = _fixture.Create<Client>();
            var consultation = _fixture.Create<Consultation>();
            await _dentisteWriter.AddDentiste(dentiste);
            await _clientWriter.AddClient(client);
            await _consultationWriter.AddConsultation(consultation);
            var rdv = _fixture.Build<RendezVous>()
                .With(x => x.Consultation_id, consultation.Consultation_id)
                .With(x => x.Client_id, client.Client_id)
                .With(x => x.Dentiste_id, dentiste.Dentiste_id)
                .With(x => x.Annule, false)
                .Create();
            //Act
            await _renderVousWriter.AddRendezVous(rdv);
            var reason = _fixture.Create<string>();
            await _rdvServices.CancelRdv(rdv.Rdv_id, reason);
            //Assert
            var rdvToBeTested = await _renderVousReader.GetRendezVousById(rdv.Rdv_id);
            var annule = rdvToBeTested.Annule;
            var annuleReason = rdvToBeTested.Reason;
            Assert.NotNull(rdvToBeTested);
            Assert.True(annule);
            Assert.Equal(reason, annuleReason);
        }

        [Fact]
        public async Task ReturnErrorIfRdvDoesNotExist()
        {
            //Arrange
            var dentiste = _fixture.Create<Dentiste>();
            var client = _fixture.Create<Client>();
            var consultation = _fixture.Create<Consultation>();
            await _dentisteWriter.AddDentiste(dentiste);
            await _clientWriter.AddClient(client);
            await _consultationWriter.AddConsultation(consultation);
            var rdv = _fixture.Build<RendezVous>()
                .With(x => x.Consultation_id, consultation.Consultation_id)
                .With(x => x.Client_id, client.Client_id)
                .With(x => x.Dentiste_id, dentiste.Dentiste_id)
                .With(x => x.Annule, false)
                .Create();
            //Act
            await _renderVousWriter.AddRendezVous(rdv);
            var reason = _fixture.Create<string>();
            //Assert
            await Assert.ThrowsAsync<Exception>(() => _rdvServices.CancelRdv(Guid.NewGuid(), reason));
        }

        [Fact]
        public async Task PayForRdvIfExists()
        {
            //Arrange
            var dentiste = _fixture.Create<Dentiste>();
            var client = _fixture.Create<Client>();
            var consultation = _fixture.Create<Consultation>();
            await _dentisteWriter.AddDentiste(dentiste);
            await _clientWriter.AddClient(client);
            await _consultationWriter.AddConsultation(consultation);
            var rdv = _fixture.Build<RendezVous>()
                .With(x => x.Consultation_id, consultation.Consultation_id)
                .With(x => x.Client_id, client.Client_id)
                .With(x => x.Dentiste_id, dentiste.Dentiste_id)
                .With(x => x.Paye, false)
                .Create();
            //Act
            await _renderVousWriter.AddRendezVous(rdv);
            await _rdvServices.PayForRdv(rdv.Rdv_id);
            //Assert
            var rdvToBeTested = await _renderVousReader.GetRendezVousById(rdv.Rdv_id);
            Assert.True(rdvToBeTested.Paye);
            Assert.NotNull(rdvToBeTested);
        }

        [Fact]
        public async Task ReturnErrorIfRdvNotFound()
        {
            //Arrange
            var dentiste = _fixture.Create<Dentiste>();
            var client = _fixture.Create<Client>();
            var consultation = _fixture.Create<Consultation>();
            await _dentisteWriter.AddDentiste(dentiste);
            await _clientWriter.AddClient(client);
            await _consultationWriter.AddConsultation(consultation);
            var rdv = _fixture.Build<RendezVous>()
                .With(x => x.Consultation_id, consultation.Consultation_id)
                .With(x => x.Client_id, client.Client_id)
                .With(x => x.Dentiste_id, dentiste.Dentiste_id)
                .With(x => x.Paye, false)
                .Create();
            //Act
            await _renderVousWriter.AddRendezVous(rdv);
            //Assert
            await Assert.ThrowsAsync<Exception>(() => _rdvServices.PayForRdv(Guid.NewGuid()));
        }
    }
}