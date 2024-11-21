using KF31_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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


    }
}
