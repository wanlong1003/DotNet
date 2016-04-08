using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace DotNet.Utilities.DataBase
{
    /// <summary>
    /// Oracle数据库帮助类
    ///     使用了Oracle问为.Net开发的全托管数据库访问组件，不再需要安装Oracle客户端或加载免安装客户端的非托管组件
    /// </summary>
    public class OracleHelper : DBHelper
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        public OracleHelper(string connectionString) 
            : base(OracleClientFactory.Instance, connectionString)
        {

        }
    }
}
