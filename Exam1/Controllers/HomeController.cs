using System.Diagnostics;
using Exam1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Exam1.Controllers
{
    public class HomeController : Controller
    {
        

        public IActionResult Index()
        {
            return View();
        }

       
    }
}
