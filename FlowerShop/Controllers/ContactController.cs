using Microsoft.AspNetCore.Mvc;

namespace FlowerShop.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index() 
        { 
            return View();
        }
    }
}
