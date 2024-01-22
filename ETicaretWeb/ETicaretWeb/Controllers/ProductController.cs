using ETicaretWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaretWeb.Controllers
{
    [IsLogin]
    public class ProductController : Controller
    {
        // Ürünler listelenecek eklenecek güncellenecek ve silinecek
        public ActionResult Index()
        {
            ETicaretEntities entities = new ETicaretEntities();
            return View(entities.Product.ToList());
        }
        public ActionResult ProductPartial()
        {
            ETicaretEntities entities = new ETicaretEntities();
            return PartialView(entities.Product.ToList());
        }
        public ActionResult Submit(string product, string cost, int categoryId, string process, int myId, System.Web.HttpPostedFileBase yuklenecekDosya)
        {
            decimal tutar;
            decimal.TryParse(cost.Replace(",", "").Replace('.', ','), out tutar);
            if (process == "insert")
            {
                string yuklemeYeri = null;
                if (yuklenecekDosya != null)
                {
                    string dosyaYolu = Path.GetFileName(yuklenecekDosya.FileName);
                    yuklemeYeri = Path.Combine(Server.MapPath("~/Pictures"), dosyaYolu);
                    yuklenecekDosya.SaveAs(yuklemeYeri);
                }
                ETicaretEntities entity = new ETicaretEntities();
                Product newProduct = new Product()
                {
                    Name = product,
                    Cost = tutar,
                    CategoryId = categoryId
                };
                if (yuklenecekDosya != null)
                {
                    newProduct.Picture = yuklenecekDosya.FileName;
                }
                entity.Product.Add(newProduct);
                entity.SaveChanges();
            }
            else if (process == "update")
            {
                string yuklemeYeri = null;
                if (yuklenecekDosya != null)
                {
                    string dosyaYolu = Path.GetFileName(yuklenecekDosya.FileName);
                    yuklemeYeri = Path.Combine(Server.MapPath("~/Pictures"), dosyaYolu);
                    yuklenecekDosya.SaveAs(yuklemeYeri);
                }
                ETicaretEntities entity = new ETicaretEntities();

                var updateProduct = entity.Product.ToList().FirstOrDefault(c => c.Id == myId);
                updateProduct.Name = product;
                updateProduct.Cost = tutar;
                updateProduct.CategoryId = categoryId;
                if (yuklenecekDosya != null)
                {
                    updateProduct.Picture = yuklenecekDosya.FileName;
                }
                entity.SaveChanges();
            }
           
            return RedirectToAction("Index");
        }
        public ActionResult ProductCrud(int id = 0)
        {
            ETicaretEntities entities = new ETicaretEntities();
            var model = entities.Product.Find(new object[] { id });
            if (model == null)
            {
                return View(new Product());
            }
            return View(model);
        }
        public ActionResult DeleteProduct(int myId)
        {
            ETicaretEntities entity = new ETicaretEntities();
            var deleteProduct = entity.Product.ToList().FirstOrDefault(c => c.Id == myId);
            entity.Product.Remove(deleteProduct);
            entity.SaveChanges();
            if (!string.IsNullOrEmpty(deleteProduct.Picture))
            {
                var yuklemeYeri = Path.Combine(Server.MapPath("~/Pictures"), deleteProduct.Picture); 
                System.IO.File.Delete(yuklemeYeri);
            }

            return Json(new JsonModel(true, "silindi"), JsonRequestBehavior.AllowGet);
        }
    }
}