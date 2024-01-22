using ETicaretWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace ETicaretWeb.Controllers
{
    [IsLogin]
    public class HomeController : Controller
    {

        //burada ürünler listelensin
        public ActionResult Index()
        {
            ETicaretEntities entities = new ETicaretEntities();
            return View(entities.Product.ToList());
        }

        public ActionResult AddToCart(int productId)
        {
            ETicaretEntities entity = new ETicaretEntities();
            var user = (ETicaretWeb.User)Session["user"];
            var cart = entity.Cart.FirstOrDefault(x=>x.UserId == user.Id &&  x.ProductId == productId);
            if (cart != null)
            {
                cart.Count = cart.Count + 1;
                cart.Cost = cart.Product.Cost * cart.Count;
                entity.SaveChanges();
            }
            else
            {
                var product = entity.Product.FirstOrDefault(p => p.Id == productId);
                Cart cartAdd = new Cart()
                {
                    UserId = user.Id,
                    ProductId = productId,
                    Cost = product.Cost,
                    Count = 1
                };
                entity.Cart.Add(cartAdd);
                entity.SaveChanges();
            }

            return Json(new JsonModel(true,"sepete eklendi"),JsonRequestBehavior.AllowGet);
        }



    }
}