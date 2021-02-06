using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    //çıplak class kalmasın yani class intherince almıyorsa ileride sıkıntılar olabilir yani gruplama yapılmalı
    public class Category:IEntity
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

    }
}
