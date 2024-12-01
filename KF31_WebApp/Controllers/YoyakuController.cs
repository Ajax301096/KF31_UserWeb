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
using System.Collections;
using Newtonsoft.Json;
using System.Net;

namespace KF31_WebApp.Controllers
{
    public class YoyakuController:Controller
    {
        
        private readonly KF31_LliM5_DataContext _context;
        public YoyakuController(KF31_LliM5_DataContext context)
        {
            _context = context;
        }


        public IActionResult Yoyaku(string BookId)
        {
            var UserID = HttpContext.Session.GetString("UserId");
            
            var member = _context.Members.Where(x=>x.userID == UserID).FirstOrDefault();    
            var books = _context.Books.AsQueryable();

            var book = books.Include(b => b.Category)
                                     .Include(b => b.Publisher)
                                     .Where(x => x.BookID == BookId).FirstOrDefault();

            if (book == null)
            {
                return NotFound("本存在してない");
            }

            var stock = _context.Stocks
                                .Include(b => b.Libraty)
                                .Where(x => x.BookID == BookId);
            var libraries = new List<Libraty>();
           foreach(var item in stock)
            {
                libraries.Add(item.Libraty);
            }
            var model = new YoyakuView
            {
                userID = UserID,
                Book = book,
                BookID = BookId,
                Stock = stock.ToList(),
                Libraries = new SelectList(libraries, "LibratyID", "LibretyName")            };

            return View(model);

           
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public IActionResult Yoyaku(YoyakuView model)
        {
            model.Book = _context.Books.Include(b => b.Category)
                                     .Include(b => b.Publisher)
                                     .Where(x => x.BookID == model.BookID).FirstOrDefault();
            Console.WriteLine(model.Book.BookID);
            model.Stock = _context.Stocks
                                .Include(b => b.Libraty)
                                .Where(x => x.BookID == model.Book.BookID)
            .ToList();
            Console.WriteLine(model.BookID);
            var libraries = new List<Libraty>();
            foreach (var item in model.Stock)
            {
                libraries.Add(item.Libraty);
            }
            model.Libraries = new SelectList(libraries, "LibratyID", "LibretyName");

            if (string.IsNullOrEmpty(model.LibratyID))
            {
                ModelState.AddModelError("LibratyID", "図書館を選択してください");
                return View(model);
            }
            else
            {
                TempData["YoyakuView"] = JsonConvert.SerializeObject(model);
                return RedirectToAction("YoyakuConfirm", "Yoyaku");
            }


        }

        public IActionResult YoyakuConfirm(YoyakuView model)
        {
            var yoyakuViewJson = TempData["YoyakuView"] as string;
            if (yoyakuViewJson != null)
            {
                model = JsonConvert.DeserializeObject<YoyakuView>(yoyakuViewJson);
                Console.WriteLine(model.userID);
                Console.WriteLine(model.Book.Book_title);
                Console.WriteLine(model.Book.BookID);
                Console.WriteLine(model.LibratyID);
                Console.WriteLine(model.Stock.Count());
                return View(model);  
            }
           
            //var UserID = HttpContext.Session.GetString("UserId");

            //var member = _context.Members.Where(x => x.userID == UserID).FirstOrDefault();
            //var books = _context.Books.AsQueryable();

            //var book = books.Include(b => b.Category)
            //                         .Include(b => b.Publisher)
            //                         .Where(x => x.BookID == BookId).FirstOrDefault();


            //if (book == null)
            //{
            //    return NotFound("本存在してない");
            //}

            //var stock = _context.Stocks
            //                    .Include(b => b.Libraty)
            //                    .Where(x => x.BookID == BookId)
            //                    .ToList();

            //var libraries = _context.Libraties
            //                         .Select(l => new { l.LibratyID, l.LibretyName })
            //                         .ToList();
            //var model = new YoyakuView
            //{
            //    userID = UserID,
            //    Member = member,
            //    Book = book,
            //    Stock = stock,
            //    Libraries = new SelectList(libraries, "LibratyID", "LibretyName")
            //};
            return View(model);


        }
    }
}
