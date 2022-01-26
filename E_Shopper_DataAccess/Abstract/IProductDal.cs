using E_Shopper_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopper_DataAccess.Abstract
{
    public interface IProductDal:IRepository<Product>
    {
        List<Product> GetProductsByCategory(string category,int page,int pageSize);

        Product GetProductDetails(int id);
        int GetCountByCategory(string category);
        Product GetByWithCategories(int id);
        void Update(Product entity, int[] categoryIds);
    }
}
