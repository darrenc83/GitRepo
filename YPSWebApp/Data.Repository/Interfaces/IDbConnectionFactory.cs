using System;
using System.Data;

namespace Data.Repository.Interfaces
{
    public interface IDbConnectionFactory:IDisposable
    {
        IDbConnection Connection { get; }       
    }
}