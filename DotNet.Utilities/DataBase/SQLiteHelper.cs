using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace DotNet.Utilities.DataBase
{
    /// <summary>
    /// SQLite数据库帮助类
    /// </summary>
    public class SQLiteHelper: DBHelper
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        public SQLiteHelper(string connectionString)
            : base(SQLiteFactory.Instance, connectionString)
        {

        }
    }
}
