using Ackee.Config.Autofac;
using Ackee.Config.Loader;
using Ackee.Core;
using Autofac;
using FluentAssertions;
using Xunit;

namespace Ackee.AutofacConfig.IntegrationTest
{
    public class AutoFacTest
    {
        [Fact]
        public void Should_wireUp_with_autofac()
        {
            var autofacBuilder=new ContainerBuilder();

            AckeeLoader.Create()
                .RegisterIocModule(new AutofacAckeeModule(autofacBuilder))
                .RegisterModule(new TestModule())
                .Install();

            var resolve = autofacBuilder.Build().Resolve<IFacadeService>();

            resolve.Should().BeOfType<TestService>();

        }
    }
}
