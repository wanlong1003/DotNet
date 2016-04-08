using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Mvc;

namespace DotNet.MvcFramework.Filter
{
    /// <summary>
    /// 日志过滤器
    ///   通过将该特性添加到Action上，实现用户操作日志的记录
    /// </summary>
    public class LogFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 要记录到日志中的参数列表(“,”或“|”分隔)， "ALL"表示全部(默认)
        /// </summary>
        public string Parameters
        {
            get;
            set;
        }

        /// <summary>
        /// 日志描述
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="description">描述</param>
        /// <param name="parameters">参数列表(“,”或“|”分隔)</param>
        public LogFilterAttribute(string description, string parameters = "ALL")
        {
            this.Description = description;
            this.Parameters = parameters;
        }

        /// <summary>
        /// 在Action方法执行之前记录日志
        /// </summary>
        /// <param name="filterContext">上下文</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            //取得要记录的参数名称
            List<string> parameters = new List<string>(this.Parameters.Split(',', '|'));

            //根据请求类型取得参数列表
            NameValueCollection tempCollection = filterContext.HttpContext.Request.HttpMethod.ToLower() == "get" ? filterContext.HttpContext.Request.QueryString : filterContext.HttpContext.Request.Form;
            
            //创建一个用于保存参数和参数值的对应字典
            IDictionary<string, object> _parameters = new Dictionary<string, object>();
            
            //遍历传回的参数
            foreach (string key in tempCollection.Keys)
            {
                if (this.Parameters == "ALL" || parameters.Contains(key))
                {
                    _parameters.Add(key, tempCollection[key]);
                }
            }

            //这里就可以将_parameters中获取的数据写入到日志中去（由用户实现）
        }
    }
}