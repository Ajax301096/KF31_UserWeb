using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using KF31_WebApp.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace KF31_WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly KF31_LliM5_DataContext _context;
        public AccountController(KF31_LliM5_DataContext context)
        {
            _context = context;
        }

        public IActionResult Account(string id)
        {
            var UserID = HttpContext.Session.GetString("UserId");
            var Password = HttpContext.Session.GetString("Password");
            var account = _context.Members.Where(x => x.userID == UserID && x.password == Password).FirstOrDefault();
            
            

            return View(account);
        }
        [HttpGet]

        public IActionResult AccountUpdate(string id)
        {
            var UserID = HttpContext.Session.GetString("UserId");
            var Password = HttpContext.Session.GetString("Password");
            var account = _context.Members.Where(x => x.userID == UserID && x.password == Password).FirstOrDefault();



            return View(account);
        }
        public IActionResult PasswordUpdate(string id)
        {
            var UserID = HttpContext.Session.GetString("UserId");
            var Password = HttpContext.Session.GetString("Password");
            var account = _context.Members.Where(x => x.userID == UserID && x.password == Password).FirstOrDefault();



            return View();
        }
        public IActionResult Success()
        {
           
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> PasswordUpdate(PassWordViewModel password)
        {
            var UserID = HttpContext.Session.GetString("UserId");
            var Password = HttpContext.Session.GetString("Password");
            var account = _context.Members.Where(x => x.userID == UserID && x.password == Password).FirstOrDefault();
            if (!ModelState.IsValid)
            {
                return View(password);
            }
           


            if (account.password != password.old_password)
            {
                ModelState.AddModelError(string.Empty, "現在パスワードが正しくありません。");
                return View(password);
            }
            bool isUpdated = UpdatePassword(password.new_Password); 
            if (isUpdated)
            {
                ViewBag.Message = "パスワードが正常に変更されました。";
                return RedirectToAction("Success", "Account"); 
            }
            else
            {
                ModelState.AddModelError(string.Empty, "パスワードの変更中にエラーが発生しました。");
                return View(password);
            }
       

        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AccountUpdate(Member member)
        {
            var UserID = HttpContext.Session.GetString("UserId");
            var Password = HttpContext.Session.GetString("Password");
            var account = _context.Members.Where(x => x.userID == UserID && x.password == Password).FirstOrDefault();
          



            //入力チェックあったら、ここに書く


            account.DisplayName = member.DisplayName;
            account.mail = member.mail;
            account.Tell = member.Tell;
            account.address = member.address;
            _context.SaveChanges();
                return RedirectToAction("Success", "Account"); 


        }
        private bool UpdatePassword(string newPassword)
        {
            var UserID = HttpContext.Session.GetString("UserId");
            var Password = HttpContext.Session.GetString("Password");
            var account = _context.Members.Where(x => x.userID == UserID && x.password == Password).FirstOrDefault();
            account.password = newPassword;
            _context.SaveChanges();
            return true;
        }





    }
}

