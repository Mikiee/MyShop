using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {

        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productsCategories;


        public ProductCategoryRepository() //Product constructor to generate list of product from cache
        {
            productsCategories = cache["productCategory"] as List<ProductCategory>;
            if (productsCategories == null)
            {
                productsCategories = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            cache["productCategory"] = productsCategories;
        }

        public void Insert(ProductCategory p)
        {
            productsCategories.Add(p);
        }

        public void Update(ProductCategory productCategory)
        {
            ProductCategory ProductToUpdate = productsCategories.Find(p => p.ID == productCategory.ID);

            if (ProductToUpdate != null)
            {
                ProductToUpdate = productCategory;
            }
            else
            {
                throw new Exception("No product found from the database!");
            }
        }

        public ProductCategory Find(string ID)
        {
            ProductCategory productCategory = productsCategories.Find(p => p.ID == ID);

            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("No product category found!");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productsCategories.AsQueryable();
        }

        public void Delete(string ID)
        {
            ProductCategory ProductToDelete = productsCategories.Find(p => p.ID == ID);

            if (ProductToDelete != null)
            {
                productsCategories.Remove(ProductToDelete);
            }
            else
            {
                throw new Exception("No product found from the database!");
            }
        }

    }
}
