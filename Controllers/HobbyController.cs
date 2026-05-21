using Microsoft.AspNetCore.Mvc;

namespace AME.Controllers
{
    public class HobbyController : Controller
    {
        public IActionResult Hobby()
        {
            return View();
        }
    }
}
