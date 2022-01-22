using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopper_Entities
{
    public class ProductCategory
    {
        //Id ler PK olacak ki aynı ürün aynı category eşleşmesin
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}

