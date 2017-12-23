using Data.Repository.Interfaces;
using System;
using Shared.Core.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;


namespace Data.Repository
{
    public sealed class TerminalRepository : BaseRepository
    {
        public TerminalRepository(IDbConnectionFactory dbConnectionFactory)
            : base(dbConnectionFactory)
        {
        }
              
        public  List<TerminalModel> GetTerminals()
        {
            var ListOfTerminals = new List<TerminalModel>();
           
            using (var connection = new SqlConnection(Connection.ConnectionString))
            {
                DataTable table = new DataTable();

                connection.Open();
                string sql = "SELECT * FROM Terminals";
                    

                using (var command = new SqlCommand(sql, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }

                    foreach (DataRow row in table.Rows)
                    {
                        
                        var terminal = new TerminalModel();
                   
                        if (!row.IsNull("Id"))
                        {
                            terminal.Id = Convert.ToInt32(row["Id"]);
                        }
                        terminal.Terminaltype = row["TerminalType"].ToString();
                        terminal.TerminalAmount = (decimal)row["TerminalAmount"];


                        ListOfTerminals.Add(terminal);

                    }
                }

            }



            return ListOfTerminals;
        }

    
    }
}
