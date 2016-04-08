using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotNet.MvcFramework.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// 该页面必须要登陆后才能访问
        /// </summary>
        /// <returns></returns>
        [Filter.LoginFilter]
        public ActionResult LoginFilter()
        {
            return View();
        }

        /// <summary>
        /// 记录日志
        ///   只记录id,name,age这三个参数
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [Filter.LogFilter("日志记录测试", "id,name,age")]
        public ActionResult LogFilter(FormCollection form)
        {
            return View();
        }

        /// <summary>
        /// 定义该Action的菜单号为001
        ///   通过添加这个过滤器实现菜单权限控制
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [Filter.FunctionFilter("001")]
        public ActionResult FunctionFilter(FormCollection form)
        {
            return View();
        }

    }
}