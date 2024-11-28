using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace KF31_WebApp.Models
{
    [Table("Publisher_table")]
    public class Publisher
    {
        public string PublisherID { get; set; }
        public string PublisherName { get; set; }
        public string Publisher_email { get; set; }
        public string Publisher_Phone { get; set; }

    }
}
