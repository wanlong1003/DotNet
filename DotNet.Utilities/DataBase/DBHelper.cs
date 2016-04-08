using System;
using System.Data;
using System.Data.Common;

namespace DotNet.Utilities.DataBase
{
    /// <summary>
    /// 数据库操作基类
    ///   实现思路：每种数据库都提供了DbProviderFactory类，可以通过DbProviderFactory创建适合自身的数据组件，我们通过传入不同的DbProviderFactory来链接不同的数据库
    /// </summary>
    public abstract class DBHelper : IDBHelper
    {
        #region 属性

        private string _connectionString;
        private DbProviderFactory _dbProviderFactory;

        /// <summary>
        /// 数据提供程序工厂
        /// </summary>
        public DbProviderFactory DbProviderFactory
        {
            get
            {
                return _dbProviderFactory;
            }
        }

        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }

        #endregion

        #region 初始化

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbProviderFactory">数据提供程序工厂</param>
        /// <param name="connectionString">链接字符串</param>
        protected DBHelper(DbProviderFactory dbProviderFactory, string connectionString)
        {
            this._connectionString = connectionString;
            this._dbProviderFactory = dbProviderFactory;
        }

        #endregion

        #region ExecuteNonQuery

        /// <summary>
        /// 执行一个SQL语句，返回受影响的行数
        /// </summary>
        /// <param name="commandText">SQL语句</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>受影响的行数</returns>
        public virtual int ExecuteNonQuery(string commandText, params DbParameter[] commandParameters)
        {
            return ExecuteNonQuery(CommandType.Text, commandText, commandParameters);
        }

        /// <summary>
        /// 执行一个指定类型的SQL脚本，返回受影响的行数
        /// </summary>
        /// <param name="commandType">脚本类型</param>
        /// <param name="commandText">SQL脚本</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>受影响的行数</returns>
        public virtual int ExecuteNonQuery(CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            using (DbConnection connection = _dbProviderFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandType = commandType;
                    command.CommandText = commandText;
                    command.Parameters.AddRange(commandParameters);
                    return ExecuteNonQuery(command);
                }
            }
        }

        /// <summary>
        /// 执行一个命令，返回受影响的行数
        /// </summary>
        /// <param name="command">命令</param>
        /// <returns>受影响的行数</returns>
        public virtual int ExecuteNonQuery(DbCommand command)
        {
            return command.ExecuteNonQuery();
        }

        /// <summary>
        /// 使用事务执行一个命令，返回受影响的行数
        /// </summary>
        /// <param name="entity">命令实体</param>
        /// <returns>受影响的行数</returns>
        public virtual int ExecuteNonQuery(CommandEntity entity)
        {
            int count = 0;
            using (DbConnection connection = _dbProviderFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                using (DbCommand command = connection.CreateCommand())
                {
                    command.Transaction = connection.BeginTransaction();
                    try
                    {
                        command.CommandType = entity.Type;
                        command.CommandText = entity.Text;
                        command.Parameters.AddRange(entity.Parameters);
                        count = ExecuteNonQuery(command);
                        command.Transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        command.Transaction.Rollback();
                        throw ex;
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// 使用事务执行多个命令，返回受影响的总行数
        /// </summary>
        /// <param name="entitys">命令实体列表</param>
        /// <returns>受影响的行数</returns>
        public virtual int ExecuteNonQuery(params CommandEntity[] entitys)
        {
            int count = 0;
            using (DbConnection connection = _dbProviderFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                using (DbCommand command = connection.CreateCommand())
                {
                    command.Transaction = connection.BeginTransaction();
                    try
                    {
                        foreach (CommandEntity entity in entitys)
                        {
                            command.CommandType = entity.Type;
                            command.CommandText = entity.Text;
                            command.Parameters.AddRange(entity.Parameters);
                            count += ExecuteNonQuery(command);
                        }
                        command.Transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        command.Transaction.Rollback();
                        throw ex;
                    }
                }
            }
            return count;
        }

        #endregion

        #region ExecuteDataSet

        /// <summary>
        /// 执行一个查询语句，返回结果集
        /// </summary>
        /// <param name="commandText">SQL语句</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>结果集</returns>
        public virtual DataSet ExecuteDataSet(string commandText, params DbParameter[] commandParameters)
        {
            return ExecuteDataSet(CommandType.Text, commandText, commandParameters);
        }

        /// <summary>
        /// 执行一个指定类型的查询脚本，返回结果集
        /// </summary>
        /// <param name="commandType">脚本类型</param>
        /// <param name="commandText">SQL脚本</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>结果集</returns>
        public virtual DataSet ExecuteDataSet(CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            using (DbConnection connection = _dbProviderFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                using (DbCommand command = connection.CreateCommand())
                {
                    command.Parameters.Clear();
                    command.CommandType = CommandType.Text;
                    command.CommandText = commandText;
                    command.Parameters.AddRange(commandParameters);
                    return ExecuteDataSet(command);
                }
            }
        }

        /// <summary>
        /// 执行一个查询命令，返回结果集
        /// </summary>
        /// <param name="command">命令</param>
        /// <returns>结果集</returns>
        public virtual DataSet ExecuteDataSet(DbCommand command)
        {
            DataSet ds = new DataSet();
            using (DbDataAdapter adapter = _dbProviderFactory.CreateDataAdapter())
            {
                adapter.SelectCommand = command;
                adapter.Fill(ds);
            }
            return ds;
        }

        #endregion

        #region ExecuteReader

        /// <summary>
        /// 执行一个查询语句，返回结果数据源
        /// </summary>
        /// <param name="commandText">查询SQL语句</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>结果数据源</returns>
        public virtual DbDataReader ExecuteReader(string commandText, params DbParameter[] commandParameters)
        {
            return ExecuteReader(CommandType.Text, commandText, commandParameters);
        }

        /// <summary>
        /// 执行一个指定类型的查询脚本，返回结果数据源
        /// </summary>
        /// <param name="commandType">脚本类型</param>
        /// <param name="commandText">查询SQL脚本</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>结果数据源</returns>
        public virtual DbDataReader ExecuteReader(CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            using (DbConnection connection = _dbProviderFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = commandText;
                    command.Parameters.AddRange(commandParameters);
                    return ExecuteReader(command);
                }
            }
        }

        /// <summary>
        /// 执行一个查询命令，返回结果数据源
        /// </summary>
        /// <param name="command">查询命令</param>
        /// <returns>结果数据源</returns>
        public virtual DbDataReader ExecuteReader(DbCommand command)
        {
            return command.ExecuteReader();
        }

        #endregion

        #region ExecuteScalar

        /// <summary>
        /// 执行一个查询语句，返回第一行第一列
        /// </summary>
        /// <param name="commandText">查询语句</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>第一行第一列</returns>
        public virtual object ExecuteScalar(string commandText, params DbParameter[] commandParameters)
        {
            return ExecuteScalar(CommandType.Text, commandText, commandParameters);
        }

        /// <summary>
        /// 执行一个指定类型的查询脚本，返回第一行第一列
        /// </summary>
        /// <param name="commandType">脚本类型</param>
        /// <param name="commandText">查询脚本</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>第一行第一列</returns>
        public virtual object ExecuteScalar(CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            using (DbConnection connection = _dbProviderFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = commandText;
                    command.Parameters.AddRange(commandParameters);
                    return ExecuteScalar(command);
                }
            }
        }

        /// <summary>
        /// 执行一个查询命令，返回第一行第一列
        /// </summary>
        /// <param name="command">查询命令</param>
        /// <returns>第一行第一列</returns>
        public virtual object ExecuteScalar(DbCommand command)
        {
            return command.ExecuteScalar();
        }

        #endregion

        #region CreateParameter

        /// <summary>
        /// 创建一个参数对象
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns>参数对象</returns>
        public virtual DbParameter CreateParameter(string name)
        {
            var parameter = _dbProviderFactory.CreateParameter();
            parameter.ParameterName = name;
            return parameter;
        }

        /// <summary>
        /// 创建一个参数对象
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        /// <returns>参数对象</returns>
        public virtual DbParameter CreateParameter(string name, object value)
        {
            var parameter = CreateParameter(name);
            parameter.Value = value;
            return parameter;
        }

        /// <summary>
        /// 创建一个参数对象
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="type">参数数据类型</param>
        /// <param name="value">参数值</param>
        /// <returns>参数对象</returns>
        public virtual DbParameter CreateParameter(string name, DbType type, object value)
        {
            var parameter = CreateParameter(name, value);
            parameter.DbType = type;
            return parameter;
        }

        /// <summary>
        /// 创建一个参数对象
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="type">参数数据类型</param>
        /// <param name="size">参数大小</param>
        /// <param name="value">参数值</param>
        /// <returns>参数对象</returns>
        public virtual DbParameter CreateParameter(string name, DbType type, int size, object value)
        {
            var parameter =CreateParameter(name, type, value);
            parameter.Size = size;
            return parameter;
        }

        /// <summary>
        /// 创建一个参数对象
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="type">参数数据类型</param>
        /// <param name="direction">参数类型</param>
        /// <returns>参数对象</returns>
        public virtual DbParameter CreateParameter(string name, DbType type, ParameterDirection direction)
        {
            var parameter = CreateParameter(name);
            parameter.DbType = type;
            parameter.Direction = direction;
            return parameter;
        }

        /// <summary>
        /// 创建一个参数对象
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="type">参数数据类型</param>
        /// <param name="direction">参数类型</param>
        /// <param name="isNullable">是否接受空值</param>
        /// <returns>参数对象</returns>
        public virtual DbParameter CreateParameter(string name, DbType type, ParameterDirection direction, bool isNullable)
        {
            var parameter = CreateParameter(name, type, direction);
            parameter.IsNullable = isNullable;
            return parameter;
        }

        #endregion

        #region Common Method

        /// <summary>
        /// 判断一个数据集是否有数据
        /// </summary>
        /// <param name="ds">数据集</param>
        /// <param name="index">判断表的索引，默认为0</param>
        /// <returns>包含数据为True，否则为False</returns>
        public virtual bool DataSetHasRecord(DataSet ds, int index = 0)
        {
            return (ds != null) && (ds.Tables.Count > index) && (ds.Tables[index].Rows.Count > 0);
        }

        #endregion
    }
}
