
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotNet.MvcFramework.Filter
{
    /// <summary>
    /// 登陆过滤器
    /// </summary>
    public class LoginFilterAttribute:AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (true)  //判断用户是否登陆（一般用过Session来判断）
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())  //判断是以AJAX方法请求
                {
                    filterContext.HttpContext.Response.Write("您没有登陆");
                    filterContext.HttpContext.Response.End();
                }
                else
                {
                    filterContext.HttpContext.Response.Redirect("~/Login.html", true);
                }
            }
        }
    }
}