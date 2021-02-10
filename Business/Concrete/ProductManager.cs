using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService //CORE'a yazılanlar her projede kullanabileceğiniz back-end kodlardır.
    {
        IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IDataResult<List<Product>> GetAll()
        {
            //iş kodları
            //yetkisi var mı?
            //IProductDal ProductDal = new IProductDal(); //bir iş sınıfı başka sınıfı new'lemez çünkü memor^y'de çalışıyor demektir bu da veritabanı veya kaybetmemek istediğimiz veriler için iyi değildir.
            if (DateTime.Now.Hour == 22) //saat 22:00'dan 23:00'a kadar ürünlerin listelenmesini kapamaya çalışıyoruz
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p=> p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            if (DateTime.Now.Hour == 23) //saat 22:00'dan 23:00'a kadar ürünlerin listelenmesini kapamaya çalışıyoruz
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        IResult IProductService.Add(Product product)
        {
            if (product.ProductName.Length < 2)
            {
                return new ErrorResult("Ürün ismi en az 2 karakter olmalıdır."); //magic strings yani standart olmayan string mesajlar
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded); //bu şekilde düzenledik magic strings'dense
        }
    }
}
