namespace YPMMS.Domain.Service.Services
{
    /// <summary>
    /// Base class for the data service classes. Defines a ConnectionString property that
    /// all data services use.
    /// </summary>
    public abstract class BaseDataService
    {
        protected BaseDataService(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected string ConnectionString { get; }
    }
}
