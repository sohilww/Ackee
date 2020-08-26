using Ackee.Config;

namespace Ackee.DataAccess.EfCore
{
    public class EfCoreModule: IAckeeModule
    {
        private readonly EfCoreConfiguration _configuration;

        public EfCoreModule(EfCoreConfigurationBuilder builder):this(builder.Build())
        {
        }
        public EfCoreModule(EfCoreConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Load(IRegistration registration)
        {
            registration.RegisterInstanceAsScoped<IConnectionStringResolver>
                (a => new ConnectionStringResolver(_configuration.ConnectionString));
        }
    }
}