using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ICategoryDal:IEntityRepository<Category> //katmanlarla çalışırken public yazmak önemli ulaşabilirlik açısından
    {
        //ürünlerle aynı şeylerden oluştuğu için ikisinin yerine geçecek generic yapı oluştursak daha mantıklı olur
        
        //List<Category> GetAll(); 
        //void Add(Category category);
        //void Update(Category category);
        //void Delete(Category categort);
        //List<Category> GetAllByCategory(int categoryId);

        //IEntityRepository generic yapısını kullandığımız için bunları iptal edebildik.
    }
}
