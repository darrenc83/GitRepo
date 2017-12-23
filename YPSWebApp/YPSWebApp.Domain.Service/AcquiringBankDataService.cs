using System;
using System.Threading.Tasks;
using Data.Repository;
using Shared.Core.Models;
using System.Collections.Generic;

namespace YPSWebApp.Domain.Service
{
    public sealed class AcquiringBankDataService:BaseDataService
    {      
        public AcquiringBankDataService(string connectionString)
            : base(connectionString)
        {
        }
        

        public  List<AcquiringBankModel> GetAcquiringBank()
        {
            using (var connectionFactory = new DbConnectionFactory(ConnectionString))
            {
                var acuiringBankRepository = new AcquiringBankRepository(connectionFactory);

                var s = acuiringBankRepository.GetAcquiringBanks();
                return s;
               
            }
        }

      
    }
}
