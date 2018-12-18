﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;

       
        public ProductRepository() //Product constructor to generate list of product from cache
        {
            products = cache["product"] as List<Product>;
            if(products == null)
            {
                products = new List<Product>();
            }
        }

        public void Commit()
        {
            cache["product"] = products;
        }

        public void Insert(Product p)
        {
            products.Add(p);
        }

        public void Update(Product product)
        {
            Product ProductToUpdate = products.Find(p => p.ID == product.ID);

            if(ProductToUpdate != null)
            {
                ProductToUpdate = product;
            }
            else
            {
                throw new Exception("No product found from the database!");
            }
        }

        public Product Find(string ID)
        {
            Product product = products.Find(p => p.ID == ID);

            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("No product found from the database!");
            }
        }

        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        public void Delete(string ID)
        {
            Product ProductToDelete = products.Find(p => p.ID == ID);

            if (ProductToDelete != null)
            {
                products.Remove(ProductToDelete);
            }
            else
            {
                throw new Exception("No product found from the database!");
            }
        }


    }
}