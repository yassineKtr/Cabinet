using DataAccess.DbAccess;
using DataAccess.Readers.Clients;
using DataAccess.Readers.Consultations;
using DataAccess.Readers.Dentists;
using DataAccess.Readers.RendezVouss;
using DataAccess.Writers.Clients;
using DataAccess.Writers.Consultations;
using DataAccess.Writers.Dentistes;
using DataAccess.Writers.RendezVouss;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Rdv;

namespace DataAccess.Tests
{
    public class TestFixture
    {
        IConfiguration config;
        public ServiceProvider ServiceProvider { get; private set; }
        public TestFixture()
        {
            var Services = new ServiceCollection();
            Services.AddSingleton<IReadClient, ClientReader>();
            Services.AddSingleton<IWriteClient, ClientWriter>();
            Services.AddSingleton<IReadConsultation, ConsultationReader>();
            Services.AddSingleton<IWriteConsultation, ConsultationWriter>();
            Services.AddSingleton<IReadDentiste, DentisteReader>();   
            Services.AddSingleton<IWriteDentiste, DentisteWriter>();
            Services.AddSingleton<IWriteRendezVous, RendezVousWriter>();
            Services.AddSingleton<IReadRendezVous, RendezVousReader>();
            Services.AddSingleton<IPostgresqlConnection, PostgresqlConnection>();
            Services.AddSingleton<IRdvServices, RdvServices>();
            Services.AddSingleton<IPostgresqlServices, PostgresqlServices>();
            config = new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.test.json")
                                     .Build();
            Services.Configure<PostgresqlConfig>(config.GetSection("ConnectionParams"));
            ServiceProvider = Services.BuildServiceProvider();
        }
    }
}
