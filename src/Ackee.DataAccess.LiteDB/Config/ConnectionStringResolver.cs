namespace Ackee.DataAccess.LiteDB.Config
{
    public class ConnectionStringResolver : IConnectionStringResolver
    {
        private string _connectionString;

        public ConnectionStringResolver(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string Get()
        {
            return _connectionString;
        }

        public void Set(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}