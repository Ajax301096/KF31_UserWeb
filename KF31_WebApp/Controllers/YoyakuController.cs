using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using KF31_WebApp.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using System.Drawing;
using EntityWorker.Core.Helper;

namespace KF31_WebApp.Controllers
{
    public class YoyakuController : Controller
    {

        private readonly KF31_LliM5_DataContext _context;
        public YoyakuController(KF31_LliM5_DataContext context)
        {
            _context = context;
        }


        public IActionResult Yoyaku(string BookId)
        {
            var UserID = HttpContext.Session.GetString("UserId");

            var member = _context.Members.Where(x => x.userID == UserID).FirstOrDefault();
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
            foreach (var item in stock)
            {
                libraries.Add(item.Libraty);
            }
            var model = new YoyakuView
            {
                userID = UserID,
                Book = book,
                BookID = BookId,
                Stock = stock.ToList(),
                Libraries = new SelectList(libraries, "LibratyID", "LibretyName")
            };

            return View(model);


        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public IActionResult Yoyaku(YoyakuView model)
        {
            model.Book = _context.Books.Include(b => b.Category)
                                     .Include(b => b.Publisher)
                                     .Where(x => x.BookID == model.BookID).FirstOrDefault();
            model.Stock = _context.Stocks
                                .Include(b => b.Libraty)
                                .Where(x => x.BookID == model.Book.BookID)
            .ToList();
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
            TempData["YoyakuViewConfirm"] = JsonConvert.SerializeObject(model);
                return View(model);
            }


            return View(model);


        }
        public IActionResult YoyakuAdd(YoyakuView model)
        {
            var yoyakuViewJson = TempData["YoyakuViewConfirm"] as string;

            if (yoyakuViewJson != null)
            {
                model = JsonConvert.DeserializeObject<YoyakuView>(yoyakuViewJson);
                Console.WriteLine(model.userID);
                Console.WriteLine(model.Book.Book_title);
                Console.WriteLine(model.Book.BookID);
                Console.WriteLine(model.LibratyID);
                Console.WriteLine(model.Stock.Count());
                var yoyaku_model = new Yoyaku();
                yoyaku_model =   Add_Item(model);
                _context.Yoyakus.Add(yoyaku_model);
                _context.SaveChanges();
                HttpContext.Session.SetString("BarcodeImage", $"data:image/png;base64,{yoyaku_model.Yoyaku_Barcode}");
                return View();
            }
            else
            {
                return View();
            }
        }
        public Yoyaku Add_Item(YoyakuView model)
        {
            var Yoyakulist = _context.Yoyakus.AsQueryable();
            Yoyakulist = Yoyakulist.Include(x => x.Member).Include(x => x.Stock);
            var YoyakuCount = Yoyakulist.Count();
            Console.WriteLine(YoyakuCount);
            var stock_item = _context.Stocks.Where(x => x.BookID == model.BookID && x.LibratyID == model.LibratyID).FirstOrDefault();
            var YoyakuID = "";
            YoyakuID = model.userID + stock_item.StockID + (YoyakuCount + 1).ToString();
            Console.WriteLine(YoyakuID);
            var YoyakuBarCode = GenerateBarcode(YoyakuID);
            var yoyaku_model = new Yoyaku()
            {
                YoyakuID = YoyakuID,
                Quantity = 1,
                userID = model.userID,
                StockID = stock_item.StockID,
                ReturnTime = model.Return_time,
                statusID = "YYK01",
                Yoyaku_Barcode = YoyakuBarCode
            };
            return yoyaku_model;

        }
        private string GenerateBarcode(string data)
        {
            var writer = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions
                {
                    Height = 100,
                    Width = 300,
                    Margin = 1
                }
            };

            var pixelData = writer.Write(data);

            using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height))
            {
                for (int y = 0; y < pixelData.Height; y++)
                {
                    for (int x = 0; x < pixelData.Width; x++)
                    {
                        var color = pixelData.Pixels[(y * pixelData.Width + x) * 4]; // Chỉ số RGBA
                        var grayScale = System.Drawing.Color.FromArgb(color, color, color);
                        bitmap.SetPixel(x, y, grayScale);
                    }
                }

                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

    }
}