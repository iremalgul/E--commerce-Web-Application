using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaretWeb.Controllers
{
    public class PurchaseController : Controller
    {
        // GET: Purchase
        public ActionResult Index()
        {
            ETicaretEntities entities = new ETicaretEntities();
            return View(entities.Purchase.ToList());
        }

        public ActionResult Detail(int id)
        {
            ETicaretEntities entities = new ETicaretEntities();
            var products = entities.PurchaseProduct.Where(x=>x.PurchaseId==id);
            return View(products.ToList());
        }
    }
}