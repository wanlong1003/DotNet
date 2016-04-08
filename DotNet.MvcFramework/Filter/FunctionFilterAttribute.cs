using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotNet.MvcFramework.Filter
{
    /// <summary>
    /// 菜单权限特性
    ///   添加该特性后即可实现对用户菜单级权限的控制
    /// </summary>
    public class FunctionFilterAttribute: ActionFilterAttribute
    {
        /// <summary>
        /// 菜单号
        /// </summary>
        public string FunctionId
        {
            get; set;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="functionId">菜单号</param>
        public FunctionFilterAttribute(string functionId)
        {
            this.FunctionId = functionId;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (true)  //判断当前用户是否对当前菜单有权限（用户实现）
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())  //如果请求为AJAX
                {
                    filterContext.HttpContext.Response.Write("您没有权限");
                    filterContext.HttpContext.Response.End();
                }
                else
                {
                    filterContext.HttpContext.Response.Redirect("没有权限的URL", true);
                }
            }
        }
    }
}