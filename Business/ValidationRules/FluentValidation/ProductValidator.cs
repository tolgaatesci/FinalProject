using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator:AbstractValidator<Product>
    {
        public ProductValidator() //kurallar constructor içene yazılır
        {
            //ctrl + k + d kodları hizaya sokar.
            //ctrl + k + c kodları //'lar
            //ctrl + k + u kodları //'dan açar.
            //RuleFor'ları aynı sayırda . ekleye ekyele yazabilirsin ama kuralların değişmesi durumunda sıkıntı yaşayabilirsin
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.ProductName).MinimumLength(2);
            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0);
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1); //belli ürünün fiyatı minimum 10 lira olmalıdır diyoruz
            RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Ürünler A harfi ile başlamalı"); //olmayan kuralı kullanmak için Must ile kullanıyoruz, StartWithA metot ismi (ürün ismi büyük A harfi ile başlamalı)
        }

        private bool StartWithA(string arg) //arg p.ProductName
        {
            return arg.StartsWith("A"); //true ya da false döner bool çünkü fonksiyon, eğer false dönerse rulefor satırı patlar.
        }
    }
}
