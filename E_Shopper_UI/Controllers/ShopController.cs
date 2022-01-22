using E_Shopper_Business.Abstract;
using E_Shopper_Entities;
using E_Shopper_UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Shopper_UI.Controllers
{
    public class ShopController : Controller
    {
        private IProductService _productService;
        public ShopController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }

        //products/telefon?page=2
        [Route("products/{category?}")]
        public IActionResult List(string category, int page=1)
        {
            const int pageSize = 3; // sabit değişken tanımlama ifadesi
            return View(new ProductListModel()
            {
                PageInfo=new PageInfo()
                {
                    TotalItems=_productService.GetCountByCategory(category),
                    CurrentPage=page,
                    ItemsPerPage=pageSize,
                    CurrentCategory=category
                },
                Products = _productService.GetProductsByCategory(category,page,pageSize)
            });;
        }
        public IActionResult Details(int? id)
        {
            if(id== null)
            {
                return NotFound();
            }
            Product product = _productService.GetProductDetails((int)id);
            if (product == null)
            {
                return NotFound();
            }

            return View(new ProductDetailsModel()
            {
                Product=product,
                Categories=product.ProductCategories.Select(i=> i.Category).ToList()
            });
        }
    }
}
