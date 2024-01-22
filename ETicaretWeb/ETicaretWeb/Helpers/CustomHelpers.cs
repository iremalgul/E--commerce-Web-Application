using ETicaretWeb.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace ETicaretWeb.Helpers
{
    public static class CustomHelpers
    {
        public static string GetUserName(this HtmlHelper helper)
        {
            return ((User)HttpContext.Current.Session["user"]).NameSurname;
        }
        public static int GetUserId(this HtmlHelper helper)
        {
            return ((User)HttpContext.Current.Session["user"]).Id;
        }
        public static List<Category> GetAllCategories(this HtmlHelper helper)
        {
            ETicaretEntities entities = new ETicaretEntities();
            return entities.Category.ToList();
        }
        public static bool AdminLogin(string username, string password)
        {
            ETicaretEntities entities = new ETicaretEntities();
            var user = entities.User.FirstOrDefault(x => x.UserName == username && x.Password == password);
            if (user != null)
            {
                HttpContext.Current.Session["user"] = user;

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}