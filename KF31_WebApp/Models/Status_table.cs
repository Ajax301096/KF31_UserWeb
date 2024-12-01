using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace KF31_WebApp.Models
{
    [Table("Status_table")]
    public class Status_table
    {
        [Key]
        public string statusID { get; set; }

        public string status { get; set; }
    }
}
