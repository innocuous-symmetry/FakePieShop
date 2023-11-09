using FakePieShop.Repositories;
using FakePieShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FakePieShop.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IPieRepository _pieRepository;

        public SearchController(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_pieRepository.AllPies);

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Pie? pie = _pieRepository.GetPieById(id);
            if (pie == null) return NotFound();
            return Ok(pie);
        }

        [HttpPost]
        public IActionResult SearchPies([FromBody] string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
                return BadRequest();

            IEnumerable<Pie> pies = new List<Pie>();

            pies = _pieRepository.SearchPies(searchQuery);
            return new JsonResult(pies);
        }
    }
}
