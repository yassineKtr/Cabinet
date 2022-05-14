using AutoFixture;
using DataAccess.Models;
using DataAccess.Readers.RendezVouss;
using DataAccess.Writers.Clients;
using DataAccess.Writers.Consultations;
using DataAccess.Writers.Dentistes;
using DataAccess.Writers.RendezVouss;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DataAccess.Tests.Readers.RendezVouss
{
    public class RendezVousReaderShould : IClassFixture<TestFixture>
    {
        private readonly IWriteRendezVous _rendezVousWriter;
        private readonly IReadRendezVous _rendezVousReader;
        private readonly IWriteDentiste _dentisteWriter;
        private readonly IWriteConsultation _consultationWriter;
        private readonly IWriteClient _clientWriter;
        private readonly Fixture _fixture;
        private ServiceProvider _serviceProvider;

        public RendezVousReaderShould(TestFixture testFixture)
        {
            _serviceProvider = testFixture.ServiceProvider;
            _rendezVousWriter = _serviceProvider.GetService<IWriteRendezVous>();
            _rendezVousReader = _serviceProvider.GetService<IReadRendezVous>();
            _dentisteWriter = _serviceProvider.GetService<IWriteDentiste>();
            _consultationWriter = _serviceProvider.GetService<IWriteConsultation>();
            _clientWriter = _serviceProvider.GetService<IWriteClient>();
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GetRendezVousById()
        {
            // Arrange
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
            //Act
            await _rendezVousWriter.AddRendezVous(rendezVous);
            var result = await _rendezVousReader.GetRendezVousById(rendezVous.Rdv_id);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetRendezVousByDate()
        {
            //Arrange
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
            rendezVous.Date_rdv = rendezVous.Date_rdv.Date;            
            //Act
            await _rendezVousWriter.AddRendezVous(rendezVous);
            var result = await _rendezVousReader.GetRendezVousByDate(rendezVous.Date_rdv.Date);
            //Assert
            Assert.Equal(rendezVous.Rdv_id, result.FirstOrDefault().Rdv_id);
        }

        [Fact]
        public async Task GetRendezVousByClientId()
        {
            //Arrange
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
            rendezVous.Date_rdv = rendezVous.Date_rdv.Date;
            //Act
            await _rendezVousWriter.AddRendezVous(rendezVous);
            var result = await _rendezVousReader.GetRendezVousByClientId(client.Client_id);
            //Assert
            Assert.Equal(rendezVous.Rdv_id, result.FirstOrDefault().Rdv_id);
        }

        [Fact]
        public async Task GetRendezVousByDentisteId()
        {
            //Arrange
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
            rendezVous.Date_rdv = rendezVous.Date_rdv.Date;
            //Act
            await _rendezVousWriter.AddRendezVous(rendezVous);
            var result = await _rendezVousReader.GetRendezVousByDentisteId(dentiste.Dentiste_id);
            //Assert
            Assert.Equal(rendezVous.Rdv_id, result.FirstOrDefault().Rdv_id);
        }

        [Fact]
        public async Task GetRendezVousByConsultationId()
        {

            //Arrange
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
            rendezVous.Date_rdv = rendezVous.Date_rdv.Date;
            //Act
            await _rendezVousWriter.AddRendezVous(rendezVous);
            var result = await _rendezVousReader.GetRendezVousByConsultationId(consultation.Consultation_id);
            //Assert
            Assert.Equal(rendezVous.Rdv_id, result.FirstOrDefault().Rdv_id);
        }
    }
}
