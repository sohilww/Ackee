namespace Ackee.Config.Loader
{
    public interface IIocModuleBuilder
    {
        AckeeLoader RegisterIocModule(IAckeeIocModule iocModule);
    }
}