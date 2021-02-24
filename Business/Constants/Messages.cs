using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages //sürekli new'lemek istemediğimiz için static yaptık
    {
        public static string ProductAdded = "Ürün eklendi.";
        public static string ProductNotAdded = "Ürün eklenemedi.";
        public static string ProductNameInvalid = "Ürün ismi geçersiz."; //public olduğu için değişkenleri PascalCase yazdık.
        public static string MaintenanceTime = "Sistem bakımda, giremezsiniz";
        public static string ProductsListed = "Ürünler listelendi.";
        public static string UnitPriceInvalid = "Birim fiyat geçersiz";
        public static string ProductCountOfCategoryError = "Bir kategoride en fazla on ürün olabilir";
        public static string ProductNameAlreadyExists = "Aynı isimde ürün eklenemez.";
        public static string CategoryLimitExceeded = "Kategori sayısı 15'i geçtiyse sisteme yeni ürün eklenemez.";
    }
}
