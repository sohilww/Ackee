using System;
using Ackee.Application.Test.Utility;
using NSubstitute;
using Ackee.Core;
using Ackee.Core.Exceptions;
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

        [Fact]
        public void Should_throw_exception_when_ServiceLocator_is_null()
        {
            Action action = () => new CommandHandlerFactory(null);

            action.Should().Throw<ArgumentNullAckeeException>();
        }

    }
}