using System;
using System.Linq;
using System.Reflection;
using Ackee.Application;
using Ackee.AspNetCore;
using Ackee.Config.Loader;
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
                    .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(ICommandHandler<>))))
                .InstancePerLifetimeScope();
        }
        public void RegisterRepositories(Assembly assembly)
        {
            _builder.RegisterAssemblyTypes(assembly)
                .Where(a => typeof(IRepository).IsAssignableFrom(a))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
        public void RegisterScoped<TImplementation, TService>()
        {
            _builder.RegisterType<TImplementation>()
                .As<TService>()
                .InstancePerLifetimeScope();
        }

        public void RegisterDecorator<TDecorator, TService>() where TDecorator : TService
        {
            _builder.RegisterDecorator<TDecorator, TService>();
        }

        public void RegisterDecorator(Type decorator, Type service)
        {
            _builder.RegisterGenericDecorator(decorator, service);

        }
        public void RegisterSingleton<TImplementation, TService>()
        {
            _builder.RegisterType<TImplementation>().As<TService>().SingleInstance();
        }

        public void RegisterInstanceAsScoped<TImplementation>(Func<IDependencyResolver,TImplementation> register,
            Action<TImplementation> releaseAction=null)
        {
             var ss= _builder.Register(context =>
                register.Invoke(new AutofacDependencyResolver(context.Resolve<ILifetimeScope>())))
                 .InstancePerLifetimeScope();

             if (releaseAction != null)
                 ss.OnRelease(releaseAction);
        }
    }
}
