using E_Shopper_Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopper_DataAccess.Concrete.EfCore
{
    public static class SeedDatabase
    {
        public static void Seed()
        {
            var context = new ShopContext();

            //Database'e uygulanmamış Migrations var mı?
            if (context.Database.GetPendingMigrations().Count() == 0)
            {
                if (context.Categories.Count() == 0)
                {
                    context.Categories.AddRange(Categories);
                }

                if(context.Products.Count() == 0)
                {
                    context.Products.AddRange(Products);
                    context.AddRange(ProductCategory);
                }

                context.SaveChanges();
            }

        }

        private static Category[] Categories =
        {
            new Category(){Name="Telefon"},
            new Category(){Name="Bilgisayar"},
            new Category(){Name="Elektronik"}
        };

        private static Product[] Products =
        {
             new Product() { Name ="Samsung Note8",ImageUrl="1.jpg",Price=11000,Description="<p>güzel telefon</p>"},
             new Product() { Name ="Samsung Note9",ImageUrl="2.jpg",Price=12000, Description="<p>güzel telefon</p>"},
             new Product() { Name ="Samsung Note10",ImageUrl="3.jpg",Price=13000,Description="<p>güzel telefon</p>"},
             new Product() { Name ="Samsung Note11",ImageUrl="4.jpg",Price=14000,Description="<p>güzel telefon</p>"},
             new Product() { Name ="Samsung Note12",ImageUrl="5.jpg",Price=15000,Description="<p>güzel telefon</p>"},
             new Product() { Name ="Samsung Note13",ImageUrl="6.jpg",Price=16000,Description="<p>güzel telefon</p>"},
             new Product() { Name ="Samsung Note14",ImageUrl="7.jpg",Price=17000,Description="<p>güzel telefon</p>"}
        };

        private static ProductCategory[] ProductCategory =
        {
            new ProductCategory(){Product=Products[0],Category=Categories[0]},
            new ProductCategory(){Product=Products[0],Category=Categories[2]},
            new ProductCategory(){Product=Products[1],Category=Categories[0]},
            new ProductCategory(){Product=Products[1],Category=Categories[1]},
            new ProductCategory(){Product=Products[2],Category=Categories[0]},
            new ProductCategory(){Product=Products[2],Category=Categories[2]},
            new ProductCategory(){Product=Products[3],Category=Categories[1]}
        };
    }
}
