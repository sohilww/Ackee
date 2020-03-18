using System;
using System.Collections.Generic;

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

        public void Install()
        {
            if(_registry == null)
                throw new NullReferenceException("registration is null");

            foreach (var ackeeModule in _modules)
            {
                ackeeModule.Load(_registry);
            }
        }
        
    }
}