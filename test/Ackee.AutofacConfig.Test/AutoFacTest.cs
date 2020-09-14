using Ackee.Application;
using Ackee.Application.Test.Utility;
using Ackee.Config.Autofac;
using Ackee.Config.Loader;
using Ackee.Core;
using Ackee.Domain.Model.EventManager;
using Autofac;
using FluentAssertions;
using System;
using Xunit;

namespace Ackee.AutofacConfig.IntegrationTest
{
    public class AutoFacTest : IDisposable
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
        public void should_resolve_registered_commandHandlers()
        {
            InstallAckee(_autofacBuilder);

            var commandHandler = ResolveCommandHandler();

            commandHandler.Should().NotBeNull();
        }

        [Fact]
        public void registerBc_return_code_of_bc()
        {
            InstallAckee(_autofacBuilder);

            var config = ResolveBcConfig();

            config.Code.Should().BeGreaterThan(1);
            config.Name.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void EventSubscriber_and_eventPublisher_is_same_in_a_same_scope()
        {
            InstallAckee(_autofacBuilder);

            var resolver = _autofacBuilder.Build();

            var subscriber = resolver.Resolve<IEventSubscriber>();
            var publisher = resolver.Resolve<IEventPublisher>();

            subscriber.Should().Be(publisher);
        }

        private BcConfig ResolveBcConfig()
        {
            return _autofacBuilder.Build().Resolve<BcConfig>();
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
                .RegisterBc(new BcConfig()
                {
                    Code = 1000,
                    Name = "Ackee.Bc"
                })
                .Install();
        }

        public void Dispose()
        {

        }
    }

}
