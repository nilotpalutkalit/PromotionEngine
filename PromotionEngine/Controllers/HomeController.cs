using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PromotionEngine.Models;

namespace PromotionEngine.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        PromotionEngineBL promotionEngineBl;

        public HomeController(ILogger<HomeController> logger, PromotionEngineBL promotionEngine)
        {
            _logger = logger;
            promotionEngineBl = promotionEngine;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult GetPriceDetails(string orders)
        {
            List<LineItem> allOrders = new List<LineItem>();
            var allLines = orders.Split('\n');
            foreach(var line in allLines)
            {
                var tokens = line.Split(' ');
                allOrders.Add (
                    new LineItem
                    {
                        ItemID = tokens[0][0],
                        Quantity = int.Parse(tokens[1])

                    }
                );
            }

            return View("Result", promotionEngineBl.GetCheckoutPrice(allOrders));
        }
    }
}
