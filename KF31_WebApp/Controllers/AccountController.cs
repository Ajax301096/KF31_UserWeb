using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using KF31_WebApp.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

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
      




    }
}

