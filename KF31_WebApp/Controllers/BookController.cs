using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using KF31_WebApp.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using static System.Reflection.Metadata.BlobBuilder;
using System.Drawing.Printing;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace KF31_WebApp.Controllers
{
    public class BookController : Controller
    {
        private readonly KF31_LliM5_DataContext _context;
        public BookController(KF31_LliM5_DataContext context)
        {
            _context = context;
        }

      

        public IActionResult BookList(string keyword,int page = 1, int pageSize = 20)
        {
            var books = _context.Books.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                books = books.Where(b => b.Book_title.Contains(keyword) || b.Book_Author.Contains(keyword));
            }
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
        public IActionResult BookDetail( string id )
        {
            var UserID = HttpContext.Session.GetString("UserId");

            var book = _context.Books.Include(b => b.Category)
                                     .Include(b => b.Publisher)
                                     .Where(x=>x.BookID == id).FirstOrDefault();

            if (book == null)
            {
                return NotFound("本存在してない");
            }
            var stock = _context.Stocks.Include(b=>b.Libraty).Where(x => x.BookID == id);
            var model = new BookDetailModel()
            {
                userID = UserID != null ? UserID : null,
                Book=book,
                Stock=stock.ToList(),
                Total =0
            };

            foreach(var item in stock)
            {
                model.Total += item.Quantity;
                model.Libraty.Add(item.Libraty);
            }
            return View(model);
        }


        //絞り組む条件

        //public IActionResult Search(SearchBookViewModel search,int page = 1, int pageSize = 20)
        //{
        //    var book = _context.Books.ToList();
        //    if (!ModelState.IsValid)
        //    {
        //        return View(new SearchBookViewModel { Books = new List<Book>(), CurrentPage = page, TotalPages = 0 });
        //    }

        //    if (!string.IsNullOrEmpty(search.keyword))
        //    {
        //        var books = _context.Books.Where(x => x.Book_title.Contains(search.keyword));

        //        // 本数
        //        var totalBooks = books.Count();

        //        // 現在のページ
        //        var booksToDisplay = books
        //            .OrderBy(b => b.Book_title)
        //            .Skip((page - 1) * pageSize) // 前のページスキップ
        //            .Take(pageSize) // 20冊取る
        //            .ToList();

        //        //  ViewModel作成
        //        var model = new SearchBookViewModel
        //        {
        //            Books = booksToDisplay,
        //            CurrentPage = page,
        //            TotalPages = (int)Math.Ceiling(totalBooks / (double)pageSize)
        //        };

        //        return View(model);
        //    }
        //    return View(new SearchBookViewModel { Books = new List<Book>(), CurrentPage = page, TotalPages = 0 });
        //}



        private string GenerateBarcode(string data)
        {
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.CODE_128,  // Định dạng CODE_128 phổ biến cho barcode
                Options = new EncodingOptions { Height = 100, Width = 300, Margin = 1 }
            };

            using (var bitmap = writer.Write(data))
            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return Convert.ToBase64String(stream.ToArray()); // Trả về chuỗi Base64
            }
        }
        public BitmapImage GenerateBarcodeImage(string barcodeBase64)
        {
            var bitmap = new BitmapImage();
            byte[] binaryData = Convert.FromBase64String(barcodeBase64);
            using (var stream = new MemoryStream(binaryData))
            {
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = stream;
                bitmap.EndInit();
            }
            return bitmap;
        }
    }
}

