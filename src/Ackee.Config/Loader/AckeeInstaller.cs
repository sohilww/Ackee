using System;
using System.Collections.Generic;
using Ackee.Core;
using Ackee.Core.Exceptions;

namespace Ackee.Config.Loader
{
    internal class AckeeInstaller
    {
        private readonly List<IAckeeModule> _modules;
        private IRegistration _registry;

        public AckeeInstaller()
        {
            _modules=new List<IAckeeModule>();
        }
        public void AddModule(IAckeeModule module)
        {
            _modules.Add(module);
        }
        public void AddIocModule(IAckeeIocModule module)
        {
            _registry = module.CreateRegistry();
            module.Load(_registry);
        }

        public void AddBc(BcConfig config)
        {
            CheckRegistryIsNotNull();

            _registry.RegisterSingleton(config);
        }
        public void Install()
        {
            CheckRegistryIsNotNull();

            foreach (var ackeeModule in _modules)
            {
                ackeeModule.Load(_registry);
            }
        }
        private void CheckRegistryIsNotNull()
        {
            if (_registry == null)
                throw new ArgumentNullAckeeException("first register ioc", "");
        }

    }
}