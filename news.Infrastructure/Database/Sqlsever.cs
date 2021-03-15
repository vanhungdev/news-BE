using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace news.Database
{
    public class Sqlsever :IQuery
    {
        
        string cs = @"Server=DESKTOP-JBBG66M\MSSQLSERVER2012;Database=NEWS;Trusted_Connection=True;MultipleActiveResultSets=true";
        public int Execute( string sql, object param = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cs))
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
                using (SqlConnection conn = new SqlConnection(cs))
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
