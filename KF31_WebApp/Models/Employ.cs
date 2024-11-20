using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace KF31_WebApp.Models
{
    [Table("Item")]
    public class Employ
    {

        [Key]
        [Display(Name = "社員ID")]
        public string EmployID { get; set; }

        [Display(Name = "社員名")]
        public string EmployName { get; set; }

      
    }
}
