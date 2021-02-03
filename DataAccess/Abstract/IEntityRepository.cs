using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    //kuralları oluştururken T yerine her şey yazılamasın isteriz, yani T yi sınırlandırmak isteriz buna generic constraint denir
    public interface IEntityRepository<T> where T:class, IEntity, new()
        //T:class dediğimizde T olanın, referans tipi olması gerektiğini söyleriz class olma zorunluluğu değil yani
        //T:IEntity dediğimzde T, IEntity olabilir ya da IEntity implemente eden bir nesne olabilir
        //T:new() dediğimizde IEntity new'lenebilmediği için onu kullanamayız yani T soyut nesne olamaz, somut bir nesne olabilmeli ve IEntity implemente eden(customer, product, category) olabilmeli
    {
                                                    //null - filtre vermeyebilirsin demek
        List<T> GetAll(Expression<Func<T,bool>>filter=null); //getAll'u filtresini ayrı ayrı ürün ve category için getirmek istediğimiz için bu şekilde LINQ ifadeleri kullanıyoruz.
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        //List<T> GetAllByCategory(int categoryId); yukarıdaki GetAll kullanırken içine filtre kullanabileceğim için bu özel filtreleme LINQ koduna ihtiyacımız yok.
    }
}
