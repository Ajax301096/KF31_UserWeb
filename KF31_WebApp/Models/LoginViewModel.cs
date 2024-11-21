using System.ComponentModel.DataAnnotations;

namespace KF31_WebApp.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "ユーザーIDは入力必須です")]
        [Display(Name = "ユーザーID")]
        public string Id { get; set; }

        [Required(ErrorMessage = "パスワードは入力必須です")]
        [Display(Name = "パスワード")]
        public string Password { get; set; }
    }
}
