﻿using Asp.NetCoreMVCCoding.Datas;
using Asp.NetCoreMVCCoding.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt.Extensions;
using System.Security.Claims;

namespace Asp.NetCoreMVCCoding.Controllers
{
    public class AccountController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IConfiguration _configuration;

        public AccountController(DatabaseContext context, IConfiguration configuration)
        {
            _context=context;
            _configuration=configuration;
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
                string hashPassword = (model.Password + _configuration.GetValue<string>("AppSettings : MD5Salt")).MD5();

                User user = _context.Users.FirstOrDefault(x => x.Username.ToLower() == model.Username.ToLower() && x.Password.ToLower() == hashPassword.ToLower());

                if(user != null)
                {
                    if (user.Locked)
                    {
                        ModelState.AddModelError("", "User is Locked");
                        return View(model);
                    }

                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim("Id", user.Id.ToString()));
                    claims.Add(new Claim("Name-Surname", user.FullName ?? string.Empty));
                    claims.Add(new Claim("Username", user.Username));

                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Home");  
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is not correct");
                }
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
                if(_context.Users.Any(x => x.Username.ToLower() == model.Username.ToLower()))
                {
                    ModelState.AddModelError("", "Username already exist");
                }

                string md5Salt = _configuration.GetValue<string>("AppSetting : MD5Salt");
                string saltedPassword = model.Password + md5Salt;
                string hashedPassword = saltedPassword.MD5();

                User user = new User();

                user.Username= model.Username;
                user.Password= hashedPassword;

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

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(Login));
        }
    }
}
