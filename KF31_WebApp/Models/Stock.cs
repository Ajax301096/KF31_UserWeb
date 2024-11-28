using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace KF31_WebApp.Models
{
    [Table("Stock_table")]
    public class Stock
    {
        [Key]
        [Display(Name = "在庫ID")]
        public string StockID { get; set; }

        [Display(Name = "本ID")]
        public string BookID { get; set; }
        [Display(Name = "数量")]
        public int Quantity { get; set; }
        [Display(Name = "図書館ID")]
        public string LibratyID { get; set; }
        [ForeignKey("BookID")]
        public virtual Book Book { get; set; }
        [ForeignKey("LibratyID")]
        public virtual Libraty Libraty { get; set; }
    }
}
