using System.ComponentModel.DataAnnotations;

namespace KF31_WebApp.Models
{
    public class SearchBookViewModel:BookListViewModel
    {
        [Required(ErrorMessage = "キーワード入力してください！")]
        [Display(Name = "キーワード")]
        public string keyword { get; set; }
    }
}
