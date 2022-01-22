using E_Shopper_Business.Abstract;
using E_Shopper_UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Shopper_UI.Controllers
{
    public class HomeController : Controller
    {
        #region Injection 
        //Program.cs
        //IProductService => ProductManager => IProductDal => MemoryProductDal

        private IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }
        #endregion

        public IActionResult Index()
        {
            //return View(_productService.GetAll());//Direk çekmek yerine Model ile ulaşım sağlayalım

            return View(new ProductListModel()
            {
                Products = _productService.GetAll()
            });
        }
    }
}
