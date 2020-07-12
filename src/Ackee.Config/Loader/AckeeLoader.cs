using Ackee.Core;

namespace Ackee.Config.Loader
{
    public class AckeeLoader : IIocModuleBuilder
    {
        private readonly AckeeInstaller _ackeeInstaller;

        public static AckeeLoader Create()
        {
            return new AckeeLoader();
        }
        public AckeeLoader()
        {
            _ackeeInstaller = new AckeeInstaller();
        }
        public AckeeLoader RegisterModule(IAckeeModule module)
        {
            _ackeeInstaller.AddModule(module);
            return this;
        }

        public AckeeLoader RegisterIocModule(IAckeeIocModule iocModule)
        {
            _ackeeInstaller.AddIocModule(iocModule);
            _ackeeInstaller.AddModule(new AckeeModule());
            return this;
        }

        public AckeeLoader RegisterBc(BcConfig config)
        {
            _ackeeInstaller.AddBc(config);
            return this;
        }

        public void Install()
        {
            _ackeeInstaller.Install();
        }
    }
}