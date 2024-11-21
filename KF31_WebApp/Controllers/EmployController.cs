using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using KF31_WebApp.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace KF31_WebApp.Controllers
{
    public class EmployController : Controller
    {
        private readonly KF31_LliM5_DataContext _context;
        public EmployController(KF31_LliM5_DataContext context)
        {
            _context = context;
        }

        //public IActionResult ItemList()
        //{
        //    var items = _context.Items.Select(i => i);
        //    return View(items);
        //}

        public IActionResult EmployList(string id)
        {
            var employ = _context.Employs.Where(i => i.EmployID == id).Select(i => i);
            return View(employ);
        }

      


        //[HttpPost]
        //[AutoValidateAntiforgeryToken]
        //public IActionResult ItemDetail(ItemDetailViewModel item)
        //{
        //    var cartitem = new CartItem();
        //    cartitem.Code = item.Code;
        //    cartitem.Name = item.Name;
        //    cartitem.Price = item.Price;
        //    cartitem.Quantity = (int)item.Quantity;

        //    string json;  //JSON形式のデータを格納する変数
        //    List<CartItem> shoppingcart;  //List型の動的配列（カート）を格納する変数

        //    json = HttpContext.Session.GetString("ShoppingCart");

        //    if (json == null)
        //    {
        //        //Sessionサービスにカートが存在しない場合、カートを作成する
        //        shoppingcart = new List<CartItem>();
        //    }
        //    else
        //    {
        //        shoppingcart = JsonSerializer.Deserialize<List<CartItem>>(json);
        //    }

        //    //カートに商品を追加し、JSONデータに変換し、Sessionサービスに格納する
        //    shoppingcart.Add(cartitem);
        //    json = JsonSerializer.Serialize(shoppingcart);
        //    HttpContext.Session.SetString("ShoppingCart", json);

        //    return RedirectToAction("Cart", "Cart");
        }
    }

