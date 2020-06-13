using System;
using LiteDB;

namespace Ackee.DataAccess.LiteDB.IntegrationTest
{
    public abstract class LiteDbBaseClassTest
    {
        protected static readonly string ConnectionString = Environment.CurrentDirectory + "\\DatabaseFile.db";
        protected readonly LiteRepository Db;
        
        protected LiteDbBaseClassTest()
        {
            
            Db=new LiteRepository(new ConnectionString(ConnectionString)
            {
                Connection = ConnectionType.Shared
            });
        }
    }
}