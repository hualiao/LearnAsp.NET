using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
    }

    public class ListDistinct
    {
        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            products.Add(new Product { ProductID = 1, Make = "Samsung", Model = "Galaxy S3" });
            products.Add(new Product { ProductID = 2, Make = "Samsung", Model = "Galaxy S4" });
            products.Add(new Product { ProductID = 3, Make = "Samsung", Model = "Galaxy S5" });
            products.Add(new Product { ProductID = 4, Make = "Apple", Model = "iPhone 5" });
            products.Add(new Product { ProductID = 5, Make = "Apple", Model = "iPhone 6" });
            products.Add(new Product { ProductID = 6, Make = "Apple", Model = "iPhone 6" });
            products.Add(new Product { ProductID = 7, Make = "HTC", Model = "Sensation" });
            products.Add(new Product { ProductID = 8, Make = "HTC", Model = "Desire" });
            products.Add(new Product { ProductID = 9, Make = "HTC", Model = "Desire" });
            products.Add(new Product { ProductID = 10, Make = "Nokia", Model = "Lumia 735" });
            products.Add(new Product { ProductID = 11, Make = "Nokia", Model = "Lumia 930" });
            products.Add(new Product { ProductID = 12, Make = "Nokia", Model = "Lumia 930" });
            products.Add(new Product { ProductID = 13, Make = "Sony", Model = "Xperia Z3" });

            return products;
        }
    }

    public class ProductComparer : IEqualityComparer<Product>
    {
        public bool Equals(Product x, Product y)
        {
            if (Object.ReferenceEquals(x, y)) return true;

            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;
            return x.Make == y.Make && x.Model == y.Model;
        }

        public int GetHashCode(Product product)
        {
            if (Object.ReferenceEquals(product, null)) return 0;
            int hashProductName = product.Make == null ? 0 : product.Make.GetHashCode();
            int hashProductCode = product.Model == null ? 0 : product.Model.GetHashCode();
            return hashProductName ^ hashProductCode;
        }
    }
}