using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Controllers
{
    public class ShoppingCardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
