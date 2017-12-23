using System;
using System.Data;

namespace YPMMS.Data.Repository.Interfaces
{
    /// <summary>
    /// Interface for a factory used to create database connections
    /// </summary>
    public interface IDbConnectionFactory : IDisposable
    {
        IDbConnection Connection { get; }
    }
}
