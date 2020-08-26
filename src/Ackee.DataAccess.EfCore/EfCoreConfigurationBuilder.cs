namespace Ackee.DataAccess.EfCore
{
    public class EfCoreConfigurationBuilder
    {
        private string _connectionString;

        public EfCoreConfigurationBuilder SetConnectionString(string connectionString)
        {
            _connectionString = connectionString;
            return this;
        }
        public EfCoreConfiguration Build()
        {
            return new EfCoreConfiguration()
            {
                ConnectionString = _connectionString
            };
        }
    }
}