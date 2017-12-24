using Data.Repository.Interfaces;
using System;
using Shared.Core.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlTypes;


namespace Data.Repository
{
    public sealed class MerchantRepository : BaseRepository
    {
        public MerchantRepository(IDbConnectionFactory dbConnectionFactory)
            : base(dbConnectionFactory)
        {
        }

        public int CreateMerchant(MerchantModel model)
        {
            var createYear = model.CreatedDate.Year;
            var createMonth = model.CreatedDate.Month;
            var createDay = model.CreatedDate.Day;
            var createHour = model.CreatedDate.Hour;
            var createMinute = model.CreatedDate.Minute;
            var createSecond = model.CreatedDate.Second;

            var sqlFormattedDate = createYear + "-" + createMonth + "-" + createDay + " " + createHour + ":" + createMinute + ":" + createSecond;

            SqlDateTime sqldatenull;
            sqldatenull = SqlDateTime.Null;
            string InsertStatement = "Insert into Merchant values (" + model.MerchantId + "," + "'" + model.MerchantName + "'," +
                                      "cast('" + sqlFormattedDate + "' as datetime)," + "'" +
                                      model.CreatedBy + "'," + sqldatenull + "," + "''" + "" +
                                      ","+model.AcquiringBankId+","+model.SellerId+") ";
            int identity;
            using (SqlConnection connection = new SqlConnection(
              Connection.ConnectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(
                        InsertStatement, connection);

                    connection.Open();
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    command.CommandText = "SELECT @@IDENTITY";

                    identity = Convert.ToInt32(command.ExecuteScalar());
                }


                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        identity = -1;
                    }
                    else
                    {
                        identity = 0;
                    }
                }


            }

            return identity;


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

        public int EditMerchant(MerchantModel model)
        {
            var createYear = model.LastUpdatedDate.Value.Year;
            var createMonth = model.LastUpdatedDate.Value.Month;
            var createDay = model.LastUpdatedDate.Value.Day;
            var createHour = model.LastUpdatedDate.Value.Hour;
            var createMinute = model.LastUpdatedDate.Value.Minute;
            var createSecond = model.LastUpdatedDate.Value.Second;
            int result;
            var sqlFormattedDate = createYear + "-" + createMonth + "-" + createDay + " " + createHour + ":" + createMinute + ":" + createSecond;



            using (var connection = new SqlConnection(Connection.ConnectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                var sql = "UPDATE Merchant Set MerchantName = " + "'" + model.MerchantName + "'" +
                              ", LastUpdatedDate = cast('" + sqlFormattedDate + "' as datetime)" +
                              ", LastUpdatedBy = " + "'" + model.LastUpdatedBy + "'" +
                              " Where Id = " + model.Id;
                command.CommandText = sql;
                command.CommandType = CommandType.Text;
                result = command.ExecuteNonQuery();
                connection.Close();


            }
            return result;
        }

        public MerchantModel GetMerchantToEdit(int id)
        {
            var merchant = new MerchantModel();
            using (var connection = new SqlConnection(Connection.ConnectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Merchant where Id = " + id;
                using (var command = new SqlCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                merchant.Id = (int)reader["Id"];
                                merchant.MerchantId = (long)reader["MerchantId"];
                                merchant.MerchantName = reader["MerchantName"].ToString();
                                merchant.CreatedBy = reader["CreatedBy"].ToString();
                                merchant.CreatedDate = (DateTime)reader["CreatedDate"];
                                merchant.LastUpdatedBy = reader["LastUpdatedBy"].ToString();
                                if (!reader.IsDBNull(5))
                                {
                                    merchant.LastUpdatedDate = (DateTime)reader["LastUpdatedDate"];
                                }

                            }
                        }
                    }

                }
            }
            return merchant;
        }

        public List<MerchantModel> GetMerchants()
        {

            var ListOfMerchants = new List<MerchantModel>();
            List<MerchantModel> merchantList = new List<MerchantModel>();
            using (var connection = new SqlConnection(Connection.ConnectionString))
            {
                DataTable table = new DataTable();

                connection.Open();
                string sql = "SELECT * FROM Merchant m " +
                    "left join Sellers s on m.SellerId=s.Id " +
                    "left join AcquiringBank ab on m.AcquiringBankId= ab.Id";

                using (var command = new SqlCommand(sql, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }

                    foreach (DataRow row in table.Rows)
                    {
                        var merchant = new MerchantModel();
                        var seller = new SellerModel();
                        var acquiringBank = new AcquiringBankModel();
                        merchant.Id = Convert.ToInt32(row["Id"]);
                        if (!row.IsNull("MerchantId"))
                        {
                            merchant.MerchantId = Convert.ToInt32(row["MerchantId"]);
                        }
                        merchant.MerchantName = row["MerchantName"].ToString();
                        merchant.CreatedBy = row["CreatedBy"].ToString();
                        if (!row.IsNull("CreatedDate"))
                        {
                            merchant.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                        }
                        merchant.LastUpdatedBy = row["LastUpdatedBy"].ToString();

                        if (!row.IsNull("SellerId"))
                        {
                            seller.Id = Convert.ToInt32(row["SellerId"]);
                        }
                        
                        seller.SellersName = row["SellersName"].ToString();
                        if (!row.IsNull("AcquiringBankId"))
                        {
                            acquiringBank.Id = Convert.ToInt32(row["AcquiringBankId"]);
                        }
                        acquiringBank.Bank = row["Bank"].ToString();
                        if (!row.IsNull("LastUpdatedDate"))
                        {
                            merchant.LastUpdatedDate = Convert.ToDateTime(row["LastUpdatedDate"]);
                        }

                        merchant.SellerModel = seller;
                        merchant.AcquiringBankModel = acquiringBank;
                        ListOfMerchants.Add(merchant);

                    }
                }
              
            }



            return ListOfMerchants;
        }
    }
}
