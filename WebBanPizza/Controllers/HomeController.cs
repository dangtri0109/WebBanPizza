using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebBanPizza.Models;

namespace WebBanPizza.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PizzaDbContext _context;

        public HomeController(ILogger<HomeController> logger, PizzaDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var vm = new HomeIndexVM
            {
                // Pizza nổi bật lấy từ DB
                FeaturedPizzas = _context.Pizzas.Take(8).ToList(),

                // 4 sản phẩm sắp ra mắt (hardcode)
                ComingSoon = new List<ComingSoonPizzaVM>
                {
                    new ComingSoonPizzaVM
                    {
                        Name = "Phô Mai Truffle",
                        Teaser = "Đậm mùi truffle, béo ngậy phô mai cao cấp.",
                        ReleaseDate = DateTime.Today.AddDays(5),
                        ImageUrl = "~/images/truffle.jpg"
                    },
                    new ComingSoonPizzaVM
                    {
                        Name = "Hải Sản Sốt Kem",
                        Teaser = "Tôm mực tươi, sốt kem béo nhẹ, thơm ngậy.",
                        ReleaseDate = DateTime.Today.AddDays(10),
                        ImageUrl = "~/images/seafood-cream.jpg"
                    },
                    new ComingSoonPizzaVM
                    {
                        Name = "Bò Nướng BBQ",
                        Teaser = "Sốt BBQ ngọt khói, thịt bò mềm mọng.",
                        ReleaseDate = DateTime.Today.AddDays(14),
                        ImageUrl = "~/images/beef-bbq.jpg"
                    },
                    new ComingSoonPizzaVM
                    {
                        Name = "Gà Teriyaki",
                        Teaser = "Vị mặn ngọt kiểu Nhật, hành tây và mè rang.",
                        ReleaseDate = DateTime.Today.AddDays(21),
                        ImageUrl = "~/images/chicken-teriyaki.jpg"
                    }
                }
            };

            return View(vm);
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
