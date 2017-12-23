using System.Linq;

namespace YPMMS.Data.Repository
{
    public static class RepositoryTools
    {
        /// <summary>
        /// Build a SQL statement that inserts a new value into a table
        /// </summary>
        /// <param name="tableName">Name of the table to insert into</param>
        /// <param name="columnNames">Names of the columns to insert into</param>
        /// <returns>SQL statement as a string</returns>
        public static string BuildInsertStatement(
            string tableName,
            params string[] columnNames)
        {
            // Build separate collection with all column names preceded with "@"
            var columnParams = columnNames.Select(c => "@" + c);

            return
                $"insert into {tableName} ({string.Join(", ", columnNames)}) values ({string.Join(", ", columnParams)})";
        }

        /// <summary>
        /// Build a SQL statement that inserts a new value into a table with an identity ID column,
        /// then retrieves the newly created row.
        /// </summary>
        /// <param name="tableName">Name of the table to insert into</param>
        /// <param name="idColumnName">Name of the table's identity ID column</param>
        /// <param name="columnNames">Names of the columns to insert into</param>
        /// <returns>SQL statement as a string</returns>
        public static string BuildInsertAndSelectStatement(
            string tableName,
            string idColumnName,
            params string[] columnNames)
        {
            return
                BuildInsertStatement(tableName, columnNames) + 
                $"; select * from {tableName} where {idColumnName} = @@IDENTITY";
        }
    }
}
