

namespace YPSWebApp.Domain.Service
{
    public abstract class BaseDataService
    {
        protected BaseDataService(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected string ConnectionString { get; }
    }
}
