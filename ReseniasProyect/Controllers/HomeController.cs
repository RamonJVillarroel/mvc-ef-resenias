using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReseniasProyect.Data;
using ReseniasProyect.Models;
using System.Diagnostics;

namespace ReseniasProyect.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ReseniasDbContex _context;
        public HomeController(ILogger<HomeController> logger, ReseniasDbContex context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var articulos = await _context.Articulos
                .Include(a => a.Categoria)
                .ToListAsync();
            var homeModel = new HomeViewModel { Articulos = articulos };
            return View(homeModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
