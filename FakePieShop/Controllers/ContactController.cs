using Microsoft.AspNetCore.Mvc;

namespace FakePieShop.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
