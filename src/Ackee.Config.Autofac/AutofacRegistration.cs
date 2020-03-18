using System;
using System.Linq;
using System.Reflection;
using Ackee.Application;
using Ackee.AspNetCore;
using Ackee.Core;
using Ackee.Domain.Model.Repositories;
using Autofac;

namespace Ackee.Config.Autofac
{
    public class AutofacRegistration : IRegistration
    {
        private readonly ContainerBuilder _builder;

        public AutofacRegistration(ContainerBuilder builder)
        {
            _builder = builder;
        }
        public void RegisterControllers(Assembly assembly)
        {
            _builder.RegisterAssemblyTypes(assembly)
                .Where(a => a.IsAssignableTo<AckeeApiController>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();


        }

        public void RegisterFacadeServices(Assembly assembly)
        {
            _builder
                .RegisterAssemblyTypes(assembly)
                .Where(a=>a.IsAssignableTo<IFacadeService>())
                .AsImplementedInterfaces().InstancePerLifetimeScope();
        }

        public void RegisterCommandHandlers(Assembly assembly)
        {
            _builder.RegisterAssemblyTypes(assembly)
                .As(type => type.GetInterfaces()
                    .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(ICommandHandler<>))));
        }
        public void RegisterRepositories(Assembly assembly)
        {
            _builder
                .RegisterAssemblyTypes(assembly)
                .Where(a => a.IsAssignableTo<IRepository>())
                .InstancePerLifetimeScope();
        }

        public void RegisterScoped<TImplementation, TService>()
        {
            _builder.RegisterType<TImplementation>()
                .As<TService>()
                .InstancePerLifetimeScope();
        }

        public void RegisterSingleton<TImplementation, TService>()
        {
            _builder.RegisterType<TImplementation>().As<TService>().SingleInstance();
        }
    }
}
