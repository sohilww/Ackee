using System;
using System.IO;
using LiteDB;

namespace Ackee.DataAccess.LiteDB.IntegrationTest
{
    public abstract class LiteDbBaseClassTest : IDisposable
    {
        protected static readonly string Path = Environment.CurrentDirectory + "\\" + GenerateDatabaseName();
        protected readonly LiteRepository Db;

        protected LiteDbBaseClassTest()
        {
            Db = new LiteRepository(new ConnectionString(Path)
            {
                Connection = ConnectionType.Shared
            });
        }
        private static string GenerateDatabaseName()
        {
            return Guid.NewGuid().ToString("N")+".db";
        }

        public void Dispose()
        {
            Db.Dispose();
            File.Delete(Path);
        }
    }
}