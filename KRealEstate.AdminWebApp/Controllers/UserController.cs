using Microsoft.AspNetCore.Mvc;

namespace KRealEstate.AdminWebApp.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
