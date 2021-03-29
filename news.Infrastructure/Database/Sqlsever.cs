using Dapper;
using Microsoft.Data.SqlClient;
using news.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace news.Database
{
    public class Sqlsever :IQuery
    {
        private static AppSettings _appSettings => AppSettingServices.Get;
        private static readonly string _connectString = _appSettings.ConnectionStringSettings.SqlServerConnectString;
        public int Execute( string sql, object param = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectString))
                {
                    conn.Open();
                    return conn.Execute(sql, param, commandType: CommandType.StoredProcedure);
                }
            }
            catch
            {
                throw;
            }
        }
        public IEnumerable<T> Query<T>(int commandType, string sql, object param = null)
        {         
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectString))
                {
                    conn.Open();
                    return conn.Query<T>(sql, param, commandType: Sqlsever.getCommandType(commandType));
                }
            }
            catch
            {
                throw;
            }
        }

        public static CommandType getCommandType(int commandType)
        {
            if (commandType == 1)
            {
                return CommandType.StoredProcedure;
            }
            else if (commandType == 2)
            {
                return CommandType.Text;
            }
            return CommandType.Text;
        }
    }
}
