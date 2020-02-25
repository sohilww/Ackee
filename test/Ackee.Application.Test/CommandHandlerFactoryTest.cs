using NSubstitute;
using System.Threading.Tasks;
using Ackee.Core;
using FluentAssertions;
using Xunit;

namespace Ackee.Application.Test
{
    public class CommandHandlerFactoryTest
    {
        [Fact]
        public void Should_get_handler_from_factory()
        {
            var serviceLocator = Substitute.For<IServiceLocator>();
            serviceLocator.Resolve<TestCommandHandler>()
                .Returns(new TestCommandHandler());

            var handlerFactory=new CommandHandlerFactory(serviceLocator);

            var handler = handlerFactory.CreateHandler(new TestCommand());

            handler.Should().NotBeOfType<TestCommandHandler>();
        }
    }

    public class TestCommandHandler : ICommandHandler<TestCommand>
    {
        public Task Handel(TestCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}