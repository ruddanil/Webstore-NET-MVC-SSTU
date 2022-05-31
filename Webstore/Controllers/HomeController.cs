using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Webstore.Models;

namespace Webstore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("ReadProducts", "Product");
        } 
    }
}