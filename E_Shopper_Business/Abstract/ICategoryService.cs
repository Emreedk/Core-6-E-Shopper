using E_Shopper_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopper_Business.Abstract
{
    internal interface ICategoryService
    {
        List<Category> GetAll();

        void Create(Category entity);
        void Update(Category entity);
        void Delete(Category entity);
    }
}
