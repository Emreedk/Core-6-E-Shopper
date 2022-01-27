using E_Shopper_Entities;
using System.ComponentModel.DataAnnotations;

namespace E_Shopper_UI.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 10, ErrorMessage = "Ürün ismi minimum 10 karakter olamlıdır.")]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }

        [Range(1, 30000)]
        [Required(ErrorMessage ="Fiyat Belirtiniz")]
        public decimal? Price { get; set; }


        public List<Category> SelectedCategories { get; set; }
    }
}
