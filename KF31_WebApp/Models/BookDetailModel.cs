using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace KF31_WebApp.Models
{
    public class BookDetailModel
    {
        public string? userID {  get; set; }
        public string BookID { get; set; }
        public List<Stock> Stock { get; set; }
        public List<Libraty> Libraty { get; set; } = new List<Libraty>();
        public int Total {  get; set; }
        [ForeignKey("BookID")]
        public virtual Book Book { get; set; }
    }
}
