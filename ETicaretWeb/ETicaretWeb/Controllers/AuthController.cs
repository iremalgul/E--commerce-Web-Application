using ETicaretWeb.Helpers;
using ETicaretWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaretWeb.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult CheckUser(string name, string password,string remember)
        {
            
            if (CustomHelpers.AdminLogin(name,password))
            {
                if (remember == "on")
                {
                    CookieHelper.CreateCookie(this.Response, Constants.COOKIEUSERNAME, name);
                    CookieHelper.CreateCookie(this.Response, Constants.COOKIEPASSWORD, password); 
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Login");
            }
            
            
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            CookieHelper.DeleteCookie(this.Response,this.Request, Constants.COOKIEUSERNAME);
            CookieHelper.DeleteCookie(this.Response, this.Request, Constants.COOKIEPASSWORD);
            return RedirectToAction("Index", "Home");
        }
    }
}