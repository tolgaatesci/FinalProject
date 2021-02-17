using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    //SOLID prensipleri, O - Open Closed Principle, yaptığın koda yeni bir özellik ekliyorsan mevcuttaki hiçbir koda dokunmazsın
    class Program
    {
        static void Main(string[] args)
        {
            //ProductTest();
            //CategoryTest();
        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal()); //EfProductDal //InMemoryProductDal
            foreach (var category in categoryManager.GetAll())
            {
                Console.WriteLine(category.CategoryName);
            }
        }

        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal()); //EfProductDal //InMemoryProductDal
            var result = productManager.GetProductDetails();

            if (result.Success == true)
            {
                foreach (var product in result.Data)
                {
                    Console.WriteLine(product.ProductName + "/" + product.CategoryName);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            //ProductManager productManager = new ProductManager(new EfProductDal());
            //var result1 = productManager.Add(new Product() { CategoryId = 9, ProductName = "rabcd", UnitsInStock = 125 });
            //var result2 = productManager.GetAll();
            //if (result1.Success == true)
            //{ 
            //    foreach (var product in result2.Data)
            //{
            //    Console.WriteLine("{0}\t {1}\t {2}\t {3}\t {4}", product.ProductId, product.CategoryId, product.ProductName, product.UnitPrice, product.UnitsInStock);
            //}
            //}
            //else
            //{
            //    Console.WriteLine(result1.Message);
            //}

            //IProductDal productDal = new EfProductDal();
            //ProductManager productManager = new ProductManager(productDal);
            //productManager.Delete(new Product { ProductId=78 });
            //var result = productManager.GetAll();
            //foreach (var product in result.Data)
            //{
            //    Console.WriteLine("{0}\t {1}\t {2}\t {3}\t {4}", product.ProductId, product.CategoryId, product.ProductName, product.UnitPrice, product.UnitsInStock);
            //}
        }
    }
}
