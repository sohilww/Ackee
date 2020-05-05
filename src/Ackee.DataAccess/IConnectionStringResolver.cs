using System;

namespace Ackee.DataAccess
{
    public interface IConnectionStringResolver
    {
        string Get();

        void Set(string connectionString);
    }
}
