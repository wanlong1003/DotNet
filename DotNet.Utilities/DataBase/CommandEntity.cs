using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace DotNet.Utilities.DataBase
{
    /// <summary>
    /// 数据库命令实体
    /// </summary>
    public class CommandEntity
    {
        public CommandEntity()
        {
            this.Type = CommandType.Text;
            this.Text = string.Empty;
            this.Parameters = null;
        }

        /// <summary>
        /// 命令类型
        /// </summary>
        public CommandType Type
        {
            get;
            set;
        }

        /// <summary>
        /// 命令脚本
        /// </summary>
        public string Text
        {
            get;
            set;
        }

        /// <summary>
        /// 命令参数
        /// </summary>
        public DbParameter[] Parameters
        {
            get;
            set;
        }

    }
}
