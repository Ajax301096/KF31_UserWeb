using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace KF31_WebApp.Models
{
    public class YoyakuView:BookDetailModel
    {
       
        public string? LibratyID { get; set; } 
        public SelectList Libraries { get; set; }
        public DateTime Return_time { get; set; }
        public string LibratyName { get; set; }

    }
}
