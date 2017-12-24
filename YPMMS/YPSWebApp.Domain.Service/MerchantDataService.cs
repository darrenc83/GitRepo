using System;
using System.Threading.Tasks;
using Data.Repository;
using Shared.Core.Models;
using System.Collections.Generic;

namespace YPSWebApp.Domain.Service
{
    public sealed class MerchantDataService:BaseDataService
    {      
        public MerchantDataService(string connectionString)
            : base(connectionString)
        {
        }


        public int CreateMerchant(MerchantModel model)
        {
            int result;            
            using (var connectionFactory = new DbConnectionFactory(ConnectionString))
            {
                var merchantRepository = new MerchantRepository(connectionFactory);
           
                result= merchantRepository.CreateMerchant(model);
           
            }

            return result;
        }

        public  List<AcquiringBankModel> GetAcquiringBank()
        {
            using (var connectionFactory = new DbConnectionFactory(ConnectionString))
            {
                var merchantRepository = new MerchantRepository(connectionFactory);

                var s =  merchantRepository.GetAcquiringBanks();
                return s;
                //    return merchant;
            }
        }

        public List<MerchantModel> GetMerchantList()
        {

            using (var connectionFactory = new DbConnectionFactory(ConnectionString))
            {
                var merchantRepository = new MerchantRepository(connectionFactory);

                var Merchants =  merchantRepository.GetMerchants();
                return Merchants;
                //    return merchant;
            }

        }

        public  MerchantModel GetMerchantToEdit(int id)
        {
            using (var connectionFactory = new DbConnectionFactory(ConnectionString))
            {
                var merchantRepository = new MerchantRepository(connectionFactory);

                var Merchant =  merchantRepository.GetMerchantToEdit(id);
                return Merchant;
               
            }
        }

        public  int EditMerchant(MerchantModel model)
        {
            using (var connectionFactory = new DbConnectionFactory(ConnectionString))
            {
                var merchantRepository = new MerchantRepository(connectionFactory);

                int result =  merchantRepository.EditMerchant(model);
                return result;
                
            }
        }
    }
}
