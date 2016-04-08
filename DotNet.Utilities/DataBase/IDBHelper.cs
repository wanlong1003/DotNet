using System.Data;
using System.Data.Common;

namespace DotNet.Utilities.DataBase
{
    /// <summary>
    /// 定义了数据库操作的全部接口
    /// </summary>
    public interface IDBHelper
    {
        #region ExecuteNonQuery

        /// <summary>
        /// 执行一个SQL语句，返回受影响的行数
        /// </summary>
        /// <param name="commandText">SQL语句</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>受影响的行数</returns>
        int ExecuteNonQuery(string commandText, params DbParameter[] commandParameters);

        /// <summary>
        /// 执行一个指定类型的SQL脚本，返回受影响的行数
        /// </summary>
        /// <param name="commandType">脚本类型</param>
        /// <param name="commandText">SQL脚本</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>受影响的行数</returns>
        int ExecuteNonQuery(CommandType commandType, string commandText, params DbParameter[] commandParameters);

        /// <summary>
        /// 执行一个命令，返回受影响的行数
        /// </summary>
        /// <param name="command">命令</param>
        /// <returns>受影响的行数</returns>
        int ExecuteNonQuery(DbCommand command);

        /// <summary>
        /// 使用事务执行一个命令，返回受影响的行数
        /// </summary>
        /// <param name="entity">命令实体</param>
        /// <returns>受影响的行数</returns>
        int ExecuteNonQuery(CommandEntity entity);

        /// <summary>
        /// 使用事务执行多个命令，返回受影响的总行数
        /// </summary>
        /// <param name="entitys">命令实体列表</param>
        /// <returns>受影响的行数</returns>
        int ExecuteNonQuery(params CommandEntity[] entitys);

        #endregion

        #region ExecuteDataSet

        /// <summary>
        /// 执行一个查询语句，返回结果集
        /// </summary>
        /// <param name="commandText">SQL语句</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>结果集</returns>
        DataSet ExecuteDataSet(string commandText, params DbParameter[] commandParameters);

        /// <summary>
        /// 执行一个指定类型的查询脚本，返回结果集
        /// </summary>
        /// <param name="commandType">脚本类型</param>
        /// <param name="commandText">SQL脚本</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>结果集</returns>
        DataSet ExecuteDataSet(CommandType commandType, string commandText, params DbParameter[] commandParameters);

        /// <summary>
        /// 执行一个查询命令，返回结果集
        /// </summary>
        /// <param name="command">命令</param>
        /// <returns>结果集</returns>
        DataSet ExecuteDataSet(DbCommand command);

        #endregion

        #region ExecuteReader

        /// <summary>
        /// 执行一个查询语句，返回结果数据源
        /// </summary>
        /// <param name="commandText">查询SQL语句</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>结果数据源</returns>
        DbDataReader ExecuteReader(string commandText, params DbParameter[] commandParameters);

        /// <summary>
        /// 执行一个指定类型的查询脚本，返回结果数据源
        /// </summary>
        /// <param name="commandType">脚本类型</param>
        /// <param name="commandText">查询SQL脚本</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>结果数据源</returns>
        DbDataReader ExecuteReader(CommandType commandType, string commandText, params DbParameter[] commandParameters);

        /// <summary>
        /// 执行一个查询命令，返回结果数据源
        /// </summary>
        /// <param name="command">查询命令</param>
        /// <returns>结果数据源</returns>
        DbDataReader ExecuteReader(DbCommand command);

        #endregion

        #region ExecuteScalar

        /// <summary>
        /// 执行一个查询语句，返回第一行第一列
        /// </summary>
        /// <param name="commandText">查询语句</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>第一行第一列</returns>
        object ExecuteScalar(string commandText, params DbParameter[] commandParameters);

        /// <summary>
        /// 执行一个指定类型的查询脚本，返回第一行第一列
        /// </summary>
        /// <param name="commandType">脚本类型</param>
        /// <param name="commandText">查询脚本</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>第一行第一列</returns>
        object ExecuteScalar(CommandType commandType, string commandText, params DbParameter[] commandParameters);

        /// <summary>
        /// 执行一个查询命令，返回第一行第一列
        /// </summary>
        /// <param name="command">查询命令</param>
        /// <returns>第一行第一列</returns>
        object ExecuteScalar(DbCommand command);

        #endregion

        #region CreateParameter

        /// <summary>
        /// 创建一个参数对象
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns>参数对象</returns>
        DbParameter CreateParameter(string name);

        /// <summary>
        /// 创建一个参数对象
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        /// <returns>参数对象</returns>
        DbParameter CreateParameter(string name, object value);

        /// <summary>
        /// 创建一个参数对象
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="type">参数数据类型</param>
        /// <param name="value">参数值</param>
        /// <returns>参数对象</returns>
        DbParameter CreateParameter(string name, DbType type, object value);

        /// <summary>
        /// 创建一个参数对象
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="type">参数数据类型</param>
        /// <param name="size">参数大小</param>
        /// <param name="value">参数值</param>
        /// <returns>参数对象</returns>
        DbParameter CreateParameter(string name, DbType type, int size, object value);

        /// <summary>
        /// 创建一个参数对象
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="type">参数数据类型</param>
        /// <param name="direction">参数类型</param>
        /// <returns>参数对象</returns>
        DbParameter CreateParameter(string name, DbType type, ParameterDirection direction);

        /// <summary>
        /// 创建一个参数对象
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="type">参数数据类型</param>
        /// <param name="direction">参数类型</param>
        /// <param name="isNullable">是否接受空值</param>
        /// <returns>参数对象</returns>
        DbParameter CreateParameter(string name, DbType type, ParameterDirection direction, bool isNullable);

        #endregion

        #region Common Method

        /// <summary>
        /// 判断一个数据集是否有数据
        /// </summary>
        /// <param name="ds">数据集</param>
        /// <param name="index">判断表的索引，默认为0</param>
        /// <returns>包含数据为True，否则为False</returns>
        bool DataSetHasRecord(DataSet ds, int index = 0);
        
        #endregion

    }
}
