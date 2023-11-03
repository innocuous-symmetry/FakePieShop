using FakePieShop.ViewModels;
using FakePieShop.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FakePieShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPieRepository _pieRepository;

        public HomeController(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }

        public IActionResult Index()
        {
            var pies = _pieRepository.PiesOfTheWeek;
            var viewModel = new HomeViewModel(pies);
            return View(viewModel);
        }
    }
}
