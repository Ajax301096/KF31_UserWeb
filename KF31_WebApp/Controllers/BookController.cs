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

        public IActionResult BookList(int page = 1, int pageSize = 20)
        {
            var books = _context.Books.AsQueryable();

            // 本数
            var totalBooks = books.Count();

            // 現在のページ
            var booksToDisplay = books
                .OrderBy(b => b.Book_title) 
                .Skip((page - 1) * pageSize) // 前のページスキップ
                .Take(pageSize) // 20冊取る
                .ToList();

            // Tạo ViewModel
            var model = new BookListViewModel
            {
                Books = booksToDisplay,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalBooks / (double)pageSize)
            };

            return View(model);
        }
        public IActionResult Search(string keyword)
        {
            var book = _context.Books.ToList();

            if (!string.IsNullOrEmpty(keyword))
            {
              var   books = _context.Books.Where(x => x.Book_title.Contains(keyword)); 
                return View(books.ToList());
            }
            return View(book);

        }




    }
}

