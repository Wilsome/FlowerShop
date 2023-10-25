using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index() 
        { 
            return View();
        }
    }
}
