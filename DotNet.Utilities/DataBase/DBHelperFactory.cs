using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.Utilities.DataBase
{
    /// <summary>
    /// 数据库帮助工厂类
    /// </summary>
    public class DBHelperFactory
    {
        /// <summary>
        /// 创建一个帮助类
        /// </summary>
        /// <param name="type">数据库类型：sqlserver/oracle/sqlite</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        public static IDBHelper Create(string type, string connectionString)
        {
            var _type = type.ToLower();
            switch (_type)
            {
                case "sqlserver":
                    return new SqlHelper(connectionString);
                case "oracle":
                    return new OracleHelper(connectionString);
                case "sqlite":
                    return new SQLiteHelper(connectionString);
                default:
                    throw new Exception("不支持的数据库类型，请检查配置");
            }
        }
    }
}
