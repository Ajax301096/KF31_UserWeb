using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using KF31_WebApp.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace KF31_WebApp.Controllers
{
    public class BookController : Controller
    {
        private readonly KF31_LliM5_DataContext _context;
        public BookController(KF31_LliM5_DataContext context)
        {
            _context = context;
        }

        //public IActionResult ItemList()
        //{
        //    var items = _context.Items.Select(i => i);
        //    return View(items);
        //}

        public IActionResult BookList(string id)
        {
            var book = _context.Books.ToList();

            return View(book);
        }




    }
}

