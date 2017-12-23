using System;
using System.Threading.Tasks;
using Data.Repository;
using Shared.Core.Models;
using System.Collections.Generic;

namespace YPSWebApp.Domain.Service
{
    public sealed class SellerDataService:BaseDataService
    {      
        public SellerDataService(string connectionString)
            : base(connectionString)
        {
        }


     

        public  List<SellerModel> GetSellers()
        {
            using (var connectionFactory = new DbConnectionFactory(ConnectionString))
            {
                var sellerRepository = new SellerRepository(connectionFactory);
                var s = sellerRepository.GetSellers();
                return s;
               
            }
        }
       
    }
}
