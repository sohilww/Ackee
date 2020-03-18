using System;
using Ackee.Application;
using Ackee.Application.Test.Utility;
using Ackee.Config.Autofac;
using Ackee.Config.Loader;
using Ackee.Core;
using Autofac;
using FluentAssertions;
using Xunit;

namespace Ackee.AutofacConfig.IntegrationTest
{
    public class AutoFacTest :IDisposable
    {
        private readonly ContainerBuilder _autofacBuilder;

        public AutoFacTest()
        {
            _autofacBuilder = new ContainerBuilder();
        }

        [Fact]
        public void Should_wireUp_with_autofac()
        {
            InstallAckee(_autofacBuilder);

            var resolve = _autofacBuilder.Build().Resolve<IFacadeService>();

            resolve.Should().BeOfType<TestService>();

        }

        [Fact]
        public void should_resolve_registred_commandHandlers()
        {
            InstallAckee(_autofacBuilder);

            var commandHandler = ResolveCommandHandler();

            commandHandler.Should().NotBeNull();
        }

        private ICommandHandler<TestCommand> ResolveCommandHandler()
        {
            return _autofacBuilder.Build().Resolve<ICommandHandler<TestCommand>>();
        }

        private static void InstallAckee(ContainerBuilder autofacBuilder)
        {
            AckeeLoader.Create()
                .RegisterIocModule(new AutofacAckeeModule(autofacBuilder))
                .RegisterModule(new TestModule())
                .Install();
        }

        public void Dispose()
        {
            
        }
    }
    
}
