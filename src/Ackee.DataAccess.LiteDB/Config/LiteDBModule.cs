using System;
using Ackee.Config;
using LiteDB;

namespace Ackee.DataAccess.LiteDB.Config
{
    public class LiteDbModule :IAckeeModule
    {
        public void Load(IRegistration registration)
        {
            var connectionString = Environment.CurrentDirectory + "\\Lite.db";

            registration.RegisterInstanceAsScoped<IConnectionStringResolver>(a => new ConnectionStringResolver(connectionString));

            registration.RegisterInstanceAsScoped(a =>
            {
                var s = a.Resolve<IConnectionStringResolver>().Get();

                return new LiteRepository(new ConnectionString()
                {
                    Filename = s,
                    Connection = ConnectionType.Shared
                });
            }, repository => repository.Dispose());
        }
    }

}