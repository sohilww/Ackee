using Ackee.Application.Test.Utility;
using Ackee.Config;
using Ackee.Core;

namespace Ackee.AutofacConfig.IntegrationTest
{
    public class TestModule : IAckeeModule
    {
        public void Load(IRegistration registration)
        {
            registration.RegisterScoped<TestService,IFacadeService>();

            registration.RegisterCommandHandlers(typeof(TestCommandHandler).Assembly);
        }
    }
}