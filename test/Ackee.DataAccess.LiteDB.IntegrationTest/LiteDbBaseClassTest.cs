using System;
using LiteDB;

namespace Ackee.DataAccess.LiteDB.IntegrationTest
{
    public abstract class LiteDbBaseClassTest
    {
        protected static readonly string ConnectionString = Environment.CurrentDirectory + "/lite.db";
        protected readonly LiteRepository Db = new LiteRepository(ConnectionString);
        
        protected LiteDbBaseClassTest()
        {
            
        }
    }
}