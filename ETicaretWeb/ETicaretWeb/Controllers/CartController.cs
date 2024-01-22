using ETicaretWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaretWeb.Controllers
{
    [IsLogin]
    public class CartController : Controller
    {
        // Burada sepet ürünleri listelenecek silinecek ve güncellenecek
        public ActionResult Index()
        {
            ETicaretEntities entities = new ETicaretEntities();
            return View(entities.Cart.ToList());
        }
        public ActionResult CartDivPArtial()
        {
            ETicaretEntities entities = new ETicaretEntities();
            return PartialView(entities.Cart.ToList());
        }
        public ActionResult CartActions(int cartId, string process)
        {
            ETicaretEntities entity = new ETicaretEntities();
            var selectedCart = entity.Cart.FirstOrDefault(c => c.Id == cartId);
            if (process == "delete")
            {
                entity.Cart.Remove(selectedCart);
                entity.SaveChanges();
                return Json(new JsonModel(true, "sepetten silindi"), JsonRequestBehavior.AllowGet);
            }
            else if (process == "increase")
            {

                selectedCart.Count = selectedCart.Count + 1;
                selectedCart.Cost = selectedCart.Product.Cost * selectedCart.Count;
                entity.SaveChanges();
                return Json(new JsonModel(true, "arttırıldı"), JsonRequestBehavior.AllowGet);
            }

            else  
            {
                if (selectedCart.Count == 1)
                {
                    entity.Cart.Remove(selectedCart);
                }
                else
                {
                    selectedCart.Count = selectedCart.Count - 1;
                    selectedCart.Cost = selectedCart.Product.Cost * selectedCart.Count;
                }
                entity.SaveChanges();
                return Json(new JsonModel(true, "azaltıldı"), JsonRequestBehavior.AllowGet);

            }
            
            
        }

        public ActionResult Buy()
        {
            ETicaretEntities entity = new ETicaretEntities();
            var user = (ETicaretWeb.User)Session["user"];
            var cartProducts = entity.Cart.Where(x => x.UserId == user.Id).ToList();

            Purchase purchase = new Purchase()
            {
                UserId = user.Id,
                PurchaseDate = DateTime.Now,
                TotalAmount = cartProducts.Sum(x => x.Cost),
            };
            entity.Purchase.Add(purchase);
            entity.SaveChanges();
            var purchaseId = purchase.Id;
            var listProduct = new List<PurchaseProduct>();
            foreach (var product in cartProducts)
            {
                PurchaseProduct purchaseProduct = new PurchaseProduct()
                {
                    PurchaseId= purchaseId,
                    ProductId= product.ProductId,
                    Count = product.Count,
                    ProductAmount=product.Cost
                };
                listProduct.Add(purchaseProduct);
                //entity.PurchaseProduct.Add(purchaseProduct);
            }
            entity.PurchaseProduct.AddRange(listProduct);
            entity.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}