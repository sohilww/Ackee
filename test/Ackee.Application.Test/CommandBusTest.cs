using System;
using NSubstitute;
using System.Threading.Tasks;
using Ackee.Application.Test.Utility;
using Ackee.Core.Exceptions;
using FluentAssertions;
using Xunit;

namespace Ackee.Application.Test
{
    public class CommandBusTest
    {
        const int Once = 1;
        [Fact]
        public async Task should_call_handle_method_on_commandHandler()
        {
            var command = new TestCommand();
            var commandHandler = CreateHandler();
            var commandBus = new CommandBus(CreateCommandHandlerFactory(commandHandler));

            await commandBus.Dispatch(command);

            await commandHandler.Received(Once).Handle(command);

        }
        [Fact]
        public void Should_throw_exception_when_factory_handler_passed_as_null()
        {
            Action action=()=> new CommandBus(null);
            action.Should().Throw<ArgumentNullAckeeException>();

        }

        private static ICommandHandler<TestCommand> CreateHandler()
        {
            return Substitute.For<ICommandHandler<TestCommand>>();
        }

        private ICommandHandlerFactory CreateCommandHandlerFactory(ICommandHandler<TestCommand> handler)
        {
            var commandFactory = Substitute.For<ICommandHandlerFactory>();
            commandFactory.CreateHandler(Arg.Any<TestCommand>()).Returns(handler);
            return commandFactory;
        }
    }
}
