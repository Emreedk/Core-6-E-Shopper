using E_Shopper_DataAccess.Abstract;
using E_Shopper_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopper_DataAccess.Concrete.EfCore
{
    internal class EfCoreCategoryDal : EfCoreGenericRepository<Category, ShopContext>, ICategoryDal
    {
      
    }
}
