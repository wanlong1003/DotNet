using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace DotNet.Utilities.DataBase
{
    /// <summary>
    /// SQLServer数据库帮助类
    /// </summary>
    public class SqlHelper: DBHelper
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        public SqlHelper(string connectionString)
            : base(SqlClientFactory.Instance, connectionString)
        {

        }
    }
}
