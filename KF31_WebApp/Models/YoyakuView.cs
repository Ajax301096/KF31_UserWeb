using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace KF31_WebApp.Models
{
    public class YoyakuView:BookDetailModel
    {
        public string userID {  get; set; }
        [ForeignKey("userID")]
        public virtual Member Member { get; set; }
        public int SelectedLibraryId { get; set; } 
        public SelectList Libraries { get; set; }
    }
}
