using E_Shopper_Entities;

namespace E_Shopper_UI.Models
{
    public class ProductDetailsModel
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
    }
}
