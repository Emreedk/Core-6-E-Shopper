using E_Shopper_Entities;

namespace E_Shopper_UI.Models
{
    public class PageInfo
    {
        public int TotalItems { get; set; }           // Toplam Ürün Sayısı
        public int ItemsPerPage { get; set; }         // Her sayfada kaç ürün görünecek
        public int CurrentPage { get; set; }          // Hangi sayfadayız
        public string CurrentCategory { get; set; }   // Hangi Category

        public int TotalPages()
        {
            return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
        }
    }
    public class ProductListModel
    {
        public List<Product> Products { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
