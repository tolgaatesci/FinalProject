using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    //SOLID prensipleri, O - Open Closed Principle, yaptığın koda yeni bir özellik ekliyorsan mevcuttaki hiçbir koda dokunmazsın
    class Program
    {
        static void Main(string[] args)
        {
            ProductManager productManager = new ProductManager(new EfProductDal()); //EfProductDal //InMemoryProductDal
            foreach (var product in productManager.GetByUnitPrice(40,100))
            {
                Console.WriteLine(product.ProductName);
            }
        }
    }
}
