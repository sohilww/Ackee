namespace Ackee.Config.Loader
{
    public interface IAckeeIocModule :IAckeeModule
    {
        IRegistration CreateRegistry();
    }
}