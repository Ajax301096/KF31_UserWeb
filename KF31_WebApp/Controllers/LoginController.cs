﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KF31_WebApp.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
namespace KF31_WebApp.Controllers
{
    public class LoginController:Controller
    {

        private readonly KF31_LliM5_DataContext _context;
        public LoginController(KF31_LliM5_DataContext context)
        {
            _context = context;
        }

        public IActionResult Login(string id)
        {

            return View();
        }
        public IActionResult LoginYoyaku(string BookId)
        {
            ViewData["BookId"] = BookId;
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginViewModel member)
        {
            //ユーザのデータベース更新するときに、ここに更新して
            var checkuser = _context.Members.Where(m => m.userID == member.Id && m.password == member.Password).FirstOrDefault();

            if (checkuser == null)
            {
                ModelState.AddModelError("Password", "IDまたはパスボート間違います！");
                return View(member);
            }

            HttpContext.Session.SetString("UserId", checkuser.userID);
            HttpContext.Session.SetString("Password", checkuser.password);

            var claims = new[] { new Claim(ClaimTypes.Name, checkuser.DisplayName) };
            var identity = new ClaimsIdentity(claims, "Cookie");
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync("Cookie", principal);

            return RedirectToAction("Account", "Account");
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> LoginYoyakuPost(LoginViewModel member,string BookId)
        {
            //ユーザのデータベース更新するときに、ここに更新して
            var checkuser = _context.Members.Where(m => m.userID == member.Id && m.password == member.Password).FirstOrDefault();

            if (checkuser == null)
            {
                ModelState.AddModelError("Password", "IDまたはパスボート間違います！");
                return View(member);
            }

            HttpContext.Session.SetString("UserId", checkuser.userID);
            HttpContext.Session.SetString("Password", checkuser.password);
            var claims = new[] { new Claim(ClaimTypes.Name, checkuser.DisplayName) };
            var identity = new ClaimsIdentity(claims, "Cookie");
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync("Cookie", principal);
            return RedirectToAction("Yoyaku", "Yoyaku",new { BookId });
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync();
          
            return Redirect("/");
        }
       

    }
}
