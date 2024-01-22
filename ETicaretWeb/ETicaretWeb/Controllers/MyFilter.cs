using ETicaretWeb.Helpers;
using ETicaretWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaretWeb.Controllers
{
    public class IsLogin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["user"] == null)
            {
                var userName = CookieHelper.GetCookie(filterContext.HttpContext.Request, Constants.COOKIEUSERNAME);
                var passwor = CookieHelper.GetCookie(filterContext.HttpContext.Request, Constants.COOKIEPASSWORD);

                if (userName == null || passwor == null)
                {
                    filterContext.Result = new RedirectResult("~/Auth/Login");
                    return;
                }
                else
                {
                    bool isLogin =  CustomHelpers.AdminLogin(userName, passwor);
                    if (isLogin)
                    {
                        base.OnActionExecuting(filterContext);
                        return;
                    }
                    else
                    {
                        filterContext.Result = new RedirectResult("~/Auth/Login");
                        return;
                    }
                }
                
            }
            base.OnActionExecuting(filterContext);
        }
    }
}