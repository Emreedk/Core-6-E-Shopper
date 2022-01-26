using E_Shopper_DataAccess.Abstract;
using E_Shopper_Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopper_DataAccess.Concrete.EfCore
{
    public class EfCoreProductDal : EfCoreGenericRepository<Product, ShopContext>, IProductDal
    {
        public List<Product> GetProductsByCategory(string category, int page, int pageSize)
        {
            using (var context = new ShopContext())
            {
                var products = context.Products.AsQueryable();//kodun kopyasını tutarki duruma göre komut geliştirilebilsin veya ToList() methodu ile bitirilsin.

                if (!string.IsNullOrEmpty(category))
                {
                    products = products
                                .Include(i => i.ProductCategories)
                                .ThenInclude(i => i.Category)
                                .Where(i => i.ProductCategories.Any(c => c.Category.Name.ToLower() == category.ToLower()));
                    //Any() methodu verilen koşula göre True veya False döndürür.
                }
                return products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public Product GetProductDetails(int id)
        {
            using (var context = new ShopContext())
            {
                return context.Products
                        .Where(p => p.Id == id)
                        .Include(i => i.ProductCategories)
                        .ThenInclude(i => i.Category)
                        .FirstOrDefault();
            }
        }

        public int GetCountByCategory(string category)
        {
            using (var context = new ShopContext())
            {
                var products = context.Products.AsQueryable();//kodun kopyasını tutarki duruma göre komut geliştirilebilsin veya ToList() methodu ile bitirilsin.

                if (!string.IsNullOrEmpty(category))
                {
                    products = products
                                .Include(i => i.ProductCategories)
                                .ThenInclude(i => i.Category)
                                .Where(i => i.ProductCategories.Any(c => c.Category.Name.ToLower() == category.ToLower()));
                }
                return products.Count();
            }
        }

        public Product GetByWithCategories(int id)
        {
            using (var context = new ShopContext())
            { 
                return context.Products
                            .Where(p=> p.Id==id)
                            .Include(i=> i.ProductCategories)
                            .ThenInclude(c=> c.Category)
                            .FirstOrDefault();
            }
        }

        public void Update(Product entity, int[] categoryIds)
        {
            using (var context = new ShopContext())
            {
                var product = context.Products
                                 .Include(i => i.ProductCategories)
                                 .FirstOrDefault(i=> i.Id==entity.Id);

                if (product != null)
                {
                    product.Name = entity.Name;
                    product.Description = entity.Description;
                    product.ImageUrl = entity.ImageUrl;
                    product.Price = entity.Price;

                    product.ProductCategories = categoryIds.Select(catid => new ProductCategory()
                    {
                        CategoryId=catid,
                        ProductId=entity.Id
                    }).ToList();

                    context.SaveChanges();

                }
            }
        }
    }
}
