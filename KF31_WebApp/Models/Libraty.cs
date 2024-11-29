using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KF31_WebApp.Models
{
    [Table("Libraty_table")]
    public class Libraty
    {
        [Key]
        [Display(Name = "図書館ID")]
        public string LibratyID { get; set; }
        [Display(Name = "図書館名")]
        public string LibretyName { get; set; }
        [Display(Name = "数量")]
        public int Book_Quantity { get; set; }
    }
}
