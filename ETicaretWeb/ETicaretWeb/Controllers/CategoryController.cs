using ETicaretWeb.Helpers;
using ETicaretWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaretWeb.Controllers
{
    [IsLogin]
    public class CategoryController : Controller
    {
        // Burada kategoriler eklenecek güncellenecek ve silinebilecek
        public ActionResult Index()
        {
            ETicaretEntities entities = new ETicaretEntities();
            return View(entities.Category.ToList());
        }
        public ActionResult CategoryPArtial() {
            ETicaretEntities entities = new ETicaretEntities();
            return PartialView(entities.Category.ToList());
        }
        public ActionResult Submit(string category, string process, int myId = 0) 
        {
            if (process == "insert")
            {
                ETicaretEntities entity = new ETicaretEntities();
                Category newCategory = new Category()
                {
                   Name = category
             
                };
                entity.Category.Add(newCategory);
                entity.SaveChanges();
                return Json(new JsonModel(true, "eklendi"), JsonRequestBehavior.AllowGet);
            }
            else if(process == "update")
            {
                ETicaretEntities entity = new ETicaretEntities();
                
                var updateCategory = entity.Category.ToList().FirstOrDefault(c => c.Id== myId);
                updateCategory.Name = category;
                entity.SaveChanges();
                return Json(new JsonModel(true, "güncellendi"), JsonRequestBehavior.AllowGet);
            }
            else
            {
              
                ETicaretEntities entity = new ETicaretEntities();
                var deleteCategory = entity.Category.ToList().FirstOrDefault(c => c.Id == myId);

                if (entity.Product.ToList().FirstOrDefault(c => c.Category==deleteCategory)!=null)
                {
                    return Json(new JsonModel(false, "silinemez"), JsonRequestBehavior.AllowGet);
                }

                entity.Category.Remove(deleteCategory);
                entity.SaveChanges();
                return Json(new JsonModel(true, "silindi"), JsonRequestBehavior.AllowGet);
            }
            
        }
    }
}