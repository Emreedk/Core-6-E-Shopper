using Microsoft.AspNetCore.Mvc;

namespace E_Shopper_UI.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
