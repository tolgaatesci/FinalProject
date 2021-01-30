﻿using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        public List<Product> GetAll()
        {
            //iş kodları
            //yetkisi var mı?
            //IProductDal ProductDal = new IProductDal(); //bir iş sınıfı başka sınıfı new'lemez çünkü memor^y'de çalışıyor demektir bu da veritabanı veya kaybetmemek istediğimiz veriler için iyi değildir.
            return _productDal.GetAll();
        }
    }
}