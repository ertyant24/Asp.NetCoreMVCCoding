using Asp.NetCoreMVCCoding.Datas;
using Asp.NetCoreMVCCoding.Models;
using Microsoft.AspNetCore.Mvc;

namespace Asp.NetCoreMVCCoding.Controllers
{
    public class AccountController : Controller
    {
        private readonly DatabaseContext _context;

        public AccountController(DatabaseContext context)
        {
            _context=context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

            }

            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                User user = new User();

                user.Username= model.Username;
                user.Password= model.Password;

                _context.Users.Add(user);
                int affectedRow = _context.SaveChanges();

                if(affectedRow == 0)
                {
                    ModelState.AddModelError("", "User can not be added.");
                }
                else
                {
                    return RedirectToAction(nameof(Login));
                }

            }

            return View(model);
        }

        public IActionResult Profile()
        {
            return View();
        }
    }
}
