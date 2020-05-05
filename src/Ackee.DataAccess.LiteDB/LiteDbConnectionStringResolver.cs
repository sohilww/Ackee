namespace Ackee.DataAccess.LiteDB
{
    public class LiteDbConnectionStringResolver :IConnectionStringResolver
    {
        private string _connectionString;

        public LiteDbConnectionStringResolver(string connectionString)
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