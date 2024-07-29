using Microsoft.AspNetCore.Mvc;
using ExcelCrudMVC.Models;
using ExcelCrudMVC.Services;
using System.Threading.Tasks;

namespace ExcelCrudMVC.Controllers
{
    public class AuthViewController : Controller
    {
        private readonly IUserService _userService;

        public AuthViewController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var response = await _userService.Authenticate(model.Email, model.Password);

            if (!response.Success)
            {
                ModelState.AddModelError(string.Empty, response.Message);
                return View(model);
            }

            // Manejo del almacenamiento del token en cookies o local storage
            return RedirectToAction("Index", "Home");
        }
    }
}
