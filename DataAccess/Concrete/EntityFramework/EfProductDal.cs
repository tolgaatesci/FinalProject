using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    //NuGet - 
    public class EfProductDal : IProductDal
    {
        public void Add(Product entity)
        {
            //IDisposable pattern implementation of c#
            using (NorthwindContext context = new NorthwindContext()) //using sayesinde new'lediğimiz şeyin işi bittikten sonra bellekten kaldırılıyor. Daha performanslı yani
            {
                var addedEntity = context.Entry(entity); //referans yakalama
                addedEntity.State = EntityState.Added; //eklenecek nesneyi hazırla, set et
                context.SaveChanges(); //ekle
            }
        }

        public void Delete(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())  
            {
                var deletedEntity = context.Entry(entity); 
                deletedEntity.State = EntityState.Deleted; 
                context.SaveChanges(); 
            }
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            using (NorthwindContext context = new NorthwindContext()) 
            {

                return context.Set<Product>().SingleOrDefault(filter);

            }
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            using (NorthwindContext context = new NorthwindContext()) 
            {
                //select * from Product (sql dilinde yani) yapıp onu listeye çevriyor yani
                return filter == null ? context.Set<Product>().ToList() : context.Set<Product>().Where(filter).ToList(); //eğer filtre null ise ilk kısım (?) çalışır değil ise diğer kısım (:) çalışır

            }
        }

        public void Update(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext()) 
            {
                var updatedEntity = context.Entry(entity); 
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges(); 
            }
        }
    }
}
