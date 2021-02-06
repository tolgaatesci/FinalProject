using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext>:IEntityRepository<TEntity> //where TEntity: class, IEntity, new() where TContext: DbContext, new() ya da böyle yazabilirsin. Shift + Enter ile var olan şekilde yazıyorsun.
        where TEntity: class, IEntity, new() 
        where TContext: DbContext, new()
    {
        public void Add(TEntity entity)
        {
            //IDisposable pattern implementation of c#
            using (TContext context = new TContext()) //using sayesinde new'lediğimiz şeyin işi bittikten sonra bellekten kaldırılıyor. Daha performanslı yani
            {
                var addedEntity = context.Entry(entity); //referans yakalama
                addedEntity.State = EntityState.Added; //eklenecek nesneyi hazırla, set et
                context.SaveChanges(); //ekle
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {

                return context.Set<TEntity>().SingleOrDefault(filter);

            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                //select * from Product (sql dilinde yani) yapıp onu listeye çevriyor yani
                return filter == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filter).ToList(); //eğer filtre null ise ilk kısım (?) çalışır değil ise diğer kısım (:) çalışır

            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
