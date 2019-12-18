using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace MigratorModule.Console.Interfaces
{
    public interface IDataAccessLayer
    {
        IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);
        long Insert<T>(IDbConnection connection, T entityToInsert, IDbTransaction transaction = null, int? commandTimeout = null) where T : class;

    }
}
