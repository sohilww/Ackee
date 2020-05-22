using System.Threading.Tasks;

namespace Ackee.Application.Test.Utility
{
    public class TestCommandHandler : ICommandHandler<TestCommand>
    {
        public Task Handle(TestCommand command)
        {
            return Task.CompletedTask;
        }
    }
}