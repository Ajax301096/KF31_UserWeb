using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace KF31_WebApp.Models
{
    [Table("Category_table")]
    public class Category
    {
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
