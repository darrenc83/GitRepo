using Data.Repository.Interfaces;
using System;

using System.Threading.Tasks;
using Shared.Core.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlTypes;
using AutoMapper;

namespace Data.Repository
{
    public sealed class SellerRepository : BaseRepository
    {
        public SellerRepository(IDbConnectionFactory dbConnectionFactory)
            : base(dbConnectionFactory)
        {
        }
      
        
        public  List<SellerModel> GetSellers()
        {
            var ListOfSellers = new List<SellerModel>();
           
            using (var connection = new SqlConnection(Connection.ConnectionString))
            {
                DataTable table = new DataTable();

                connection.Open();
                string sql = "SELECT * FROM Sellers";
                    

                using (var command = new SqlCommand(sql, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }

                    foreach (DataRow row in table.Rows)
                    {
                        
                        var seller = new SellerModel();
                   
                        if (!row.IsNull("Id"))
                        {
                            seller.Id = Convert.ToInt32(row["Id"]);
                        }
                        seller.SellersName = row["SellersName"].ToString();


                        ListOfSellers.Add(seller);

                    }
                }

            }



            return ListOfSellers;
        }

    
    }
}
