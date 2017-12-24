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
    public sealed class AcquiringBankRepository : BaseRepository
    {
        public AcquiringBankRepository(IDbConnectionFactory dbConnectionFactory)
            : base(dbConnectionFactory)
        {
        }
      
        
        public  List<AcquiringBankModel> GetAcquiringBanks()
        {
            var ListOfBanks = new List<AcquiringBankModel>();
           
            using (var connection = new SqlConnection(Connection.ConnectionString))
            {
                DataTable table = new DataTable();

                connection.Open();
                string sql = "SELECT * FROM AcquiringBank";
                    

                using (var command = new SqlCommand(sql, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }

                    foreach (DataRow row in table.Rows)
                    {
                        
                        var acquiringBank = new AcquiringBankModel();
                   
                        if (!row.IsNull("Id"))
                        {
                            acquiringBank.Id = Convert.ToInt32(row["Id"]);
                        }
                        acquiringBank.Bank = row["Bank"].ToString();                        

                      
                        ListOfBanks.Add(acquiringBank);

                    }
                }

            }



            return ListOfBanks;
        }

    
    }
}
