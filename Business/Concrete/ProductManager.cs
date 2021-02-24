using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService //CORE'a yazılanlar her projede kullanabileceğiniz back-end kodlardır.
    {
        IProductDal _productDal; //bir entity manager(IProductManager) kendisinin(IProductDal) hariç başla bir dal'ı enjekte edemez. Ama başka bir servisi enjekte edebiliriz. Yani kuralımızı servise yazmamız gerekiyor. Çağrıcağımız kural servisten gelecek.
        ICategoryService _categoryService;
        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
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

        [ValidationAspect(typeof(ProductValidator))] //add metodunu doğrula ProductValidator kullanarak
        public IResult Add(Product product) //add metodunu doğrula ProductValidator kullanarak
        {
            //business codes (iş kuralları) ayrı validation (doğrulama) ayrı kullanılır
            //validation, girilen veya bir yerden çekilen verinin yapısal uyumu ile alakalı olan her şeye doğrulama deniyor
            //business codes,iş gereksinimlerimize, iş ihtiyaçlarımıza uygunluğunu ile olan her şeye deniyor.
            //Aşağıdaki kodlar validation'a girer. if'li 2 yapı ancak biz daha profesyonel olarak validationTool yapısını kurduk
            //kurduğumuz validationTool'u Add fonksiyonu gibi iş kodlarının olduğu yere koymak yerine attribute ile kullanacağız
            //add'i çağırdığımızda belli kurala uyan attribute var ise add'den önce onları çalıştırarak sonra add metoduna geçer

            //if (product.UnitPrice <= 0)
            //{
            //    return new ErrorResult(Messages.UnitPriceInvalid);
            //}
            //if (product.ProductName.Length < 2)
            //{
            //    return new ErrorResult("Ürün ismi en az 2 karakter olmalıdır."); //magic strings yani standart olmayan string mesajlar
            //}


            //ValidationTool.Validate(new ProductValidator(), product); //[ValidationAspect(typeof(ProductValidator))] olduğundan bu satıra gerek kalmadı. 

            //loglama, mesela yapılan operasyonların bir yerde kaydını tutmak
            //cacheremove
            //perfonmance
            //transaction
            //yetkilendirme

            //***********bir kategoride en fazla on ürün olabilir.*************//
            //if(CheckIfProductCountOfCategoryCorrect(product.CategoryId).Success)
            //{
            //    //***********Aynı isimde ürün eklenemez.*************//
            //    if (CheckIfProductNameExists(product.ProductName).Success)
            //    {
            //        _productDal.Add(product);
            //        return new SuccessResult(Messages.ProductAdded);
            //    }
            //    //***********Aynı isimde ürün eklenemez.*************//
            //}
            //return new ErrorResult();
            //***********bir kategoride en fazla on ürün olabilir.*************//

            IResult result = BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryId), CheckIfProductNameExists(product.ProductName), CheckIfCategoryLimitExceeded());

            if(result!=null)
            {
                return result;
            }

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded); //bu şekilde düzenledik magic strings'dense


            //var carRentalStatus = _productDal.Get(r => r.UnitsInStock == product.UnitsInStock);
            //if (carRentalStatus.CategoryId == 4)
            //{
            //    return new ErrorResult(Messages.ProductNotAdded);
            //}
            //_productDal.Add(product);
            //return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult("Silindi");
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            throw new NotImplementedException();
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId) //iş kuralı parçacığı olduğu için private yaptık, eğer farklı manager'lerde de kullanacaksak public değil interface'de metod yazarsın
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameExists(string productName) 
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }
        private IResult CheckIfCategoryLimitExceeded() //Eğer kategori sayısı 15'i geçtiyse sisteme yeni ürün eklenemez.
        {
            var result = _categoryService.GetAll();

            if (result.Data.Count>15)
            {
                return new ErrorResult(Messages.CategoryLimitExceeded);
            }
            return new SuccessResult();
        }
    }
}
