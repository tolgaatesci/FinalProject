using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    //interface'in operasyonları public'dir kendisi değil yani List<Product> GetAll(); void Add(Product product); void Update(Product product); void Delete(Product product); public'tir.
    public interface IProductDal:IEntityRepository<Product> //product tablosunun dalını yazıyoruz yani isimlendirirken böyle şablonla isimlendiriyoruz. I da interface olduğunu gösteriyor.
    {
        //List<Product> GetAll(); //ürünleri getir
        //void Add(Product product); 
        //void Update(Product product);
        //void Delete(Product product);
        //List<Product> GetAllByCategory(int categoryId); //ürünleri kategoriye göre filtrele

        //IEntityRepository generic yapısını kullandığımız için bunları iptal edebildik.

        //Code refactoring
        List<ProductDetailDto> GetProductDetails();
    }
}
