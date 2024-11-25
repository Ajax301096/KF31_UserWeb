using System.ComponentModel.DataAnnotations;

namespace KF31_WebApp.Models
{
    public class PassWordViewModel
    {
        [Required(ErrorMessage = "現在のパスワードは入力必須です")]
        [Display(Name = "現在のパスワード")]
        public string old_password { get; set; }

        [Required(ErrorMessage = "新しいパスワードは入力必須です")]
        [Display(Name = "新しいパスワード")]
        public string new_Password { get; set; }
    }
}
