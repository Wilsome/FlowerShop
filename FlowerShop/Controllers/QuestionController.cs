using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace FlowerShop.Controllers
{
    public class QuestionController : Controller
    {
        public IActionResult Index() 
        {
            return View();
        }
    }
}
