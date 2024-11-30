using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using KF31_WebApp.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KF31_WebApp.Controllers
{
    public class YoyakuController:Controller
    {
        private readonly KF31_LliM5_DataContext _context;
        public YoyakuController(KF31_LliM5_DataContext context)
        {
            _context = context;
        }

        public IActionResult Yoyaku(string id)
        {
            var UserID = HttpContext.Session.GetString("UserId");
            
            var member = _context.Members.Where(x=>x.userID == UserID).FirstOrDefault();    
            var books = _context.Books.AsQueryable();

            var book = books.Include(b => b.Category)
                                     .Include(b => b.Publisher)
                                     .Where(x => x.BookID == id).FirstOrDefault();


            if (book == null)
            {
                return NotFound("本存在してない");
            }

            var stock = _context.Stocks
                                .Include(b => b.Libraty)
                                .Where(x => x.BookID == id)
                                .ToList();

            var libraries = _context.Libraties 
                                     .Select(l => new { l.LibratyID, l.LibretyName })
                                     .ToList();
            var model = new YoyakuView
            {
                userID = UserID,
                Member = member,
                Book = book,
                Stock = stock,
                Libraries = new SelectList(libraries, "LibratyID", "LibretyName"), 
                SelectedLibraryId = 0 
            };
            Console.WriteLine(model.Book.Category.CategoryName);
            return View(model);

           
        }
    }
}
