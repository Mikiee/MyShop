using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        InMemoryRepository<ProductCategory> context;

        public ProductCategoryManagerController()
        {
            context = new InMemoryRepository<ProductCategory>();
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = context.Collection().ToList();
            return View(productCategories);
        }

        public ActionResult Create()
        {
            ProductCategory productCat = new ProductCategory();
            return View(productCat);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory productCat)
        {
            if (!ModelState.IsValid)
            {
                return View(productCat);
            }
            else
            {
                context.Insert(productCat);
                context.Commit();

                return RedirectToAction("Index");

            }
        }

        public ActionResult Edit(string ID)
        {
            ProductCategory productCat = context.Find(ID);
            if (productCat == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCat);
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory productCat, string ID)
        {
            ProductCategory ProductToEdit = context.Find(ID);
            if (ProductToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productCat);
                }


                ProductToEdit.Category = productCat.Category;
                context.Commit();

                return RedirectToAction("Index");

            }
        }

        public ActionResult Delete(string ID)
        {
            ProductCategory productToDelete = context.Find(ID);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string ID)
        {

            ProductCategory productToDelete = context.Find(ID);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(ID);
                context.Commit();
                return RedirectToAction("Index");
            }

        }
    }
}