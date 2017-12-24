using System;
using System.Threading.Tasks;
using Data.Repository;
using Shared.Core.Models;
using System.Collections.Generic;

namespace YPSWebApp.Domain.Service
{
    public sealed class TerminalDataService : BaseDataService
    {      
        public TerminalDataService(string connectionString)
            : base(connectionString)
        {
        }


       

        public  List<TerminalModel> GetTerminals()
        {
            using (var connectionFactory = new DbConnectionFactory(ConnectionString))
            {
                var terminalRepository = new TerminalRepository(connectionFactory);

                var terminals = terminalRepository.GetTerminals();
                return terminals;
                //    return merchant;
            }
        }

      
    }
}
