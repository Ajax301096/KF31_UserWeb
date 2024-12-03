using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace KF31_WebApp.Models
{
    [Table("Yoyaku_table")]
    public class Yoyaku
    {

        [Key]
        [Display(Name = "予約ID")]
        public string YoyakuID { get; set; }

        [Display(Name = "数量")]
        public int Quantity { get; set; }
        [Display(Name = "ユーザID")]
        public string userID { get; set; }
        [Display(Name = "在庫ID")]
        public string StockID { get; set; }
        [Display(Name = "返却日")]
        public DateTime ReturnTime { get; set; }
        [Display(Name = "状態")]
        public string statusID { get; set; }
        [Display(Name = "バーコード")]
        public string? Yoyaku_Barcode { get; set; }
        public DateTime start_time { get; set; }
        [ForeignKey("statusID")]
        public virtual Status_table Status { get; set; }

        [ForeignKey("userID")]
        public virtual Member Member { get; set; }
        [ForeignKey("StockID")]
        public virtual Stock Stock { get; set; }

    }
}
