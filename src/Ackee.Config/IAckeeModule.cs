namespace Ackee.Config
{
    public interface IAckeeModule
    {
        void Load(IRegistration registration);
    }
}