using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MigratorModule.Console.Interfaces;
using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;
namespace MigratorModule.Console
{
    public class DataAccessLayer : IDataAccessLayer
    {
        public SqlConnectionStringBuilder fromConnection { get; set; }
        public SqlConnectionStringBuilder toConnection { get; set; }


        public DataAccessLayer(SqlConnectionStringBuilder fromConnection, SqlConnectionStringBuilder toConnection)
        {
            this.fromConnection = fromConnection;
            this.toConnection = toConnection;
        }
        public IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (IDbConnection connection = new SqlConnection(fromConnection.ConnectionString))
            {
                return connection.Query<T>(sql, param, commandType: CommandType.Text);
            }
        }

        public long Insert<T>(IDbConnection connection, T entityToInsert, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
                return connection.Insert(entityToInsert, transaction);
        }
    }
}
