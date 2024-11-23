using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace KF31_WebApp.Models
{
    [Table("User_table")]
    public class Member
    {
        //ユーザのデータベース更新するときに、ここに更新して

        [Key]
        [Display(Name = "会員ID")]
        public string userID { get; set; }

        [Display(Name = "パスボート")]
        public string password { get; set; }

        [Display(Name = "生年月日")]
        public DateTime Birthday { get; set; }
        [Display(Name = "氏名")]
        public string DisplayName { get; set; }
        [Display(Name = "電話番号")]
        public string Tell { get; set; }
        [Display(Name = "メール")]
        public string mail { get; set; }
        [Display(Name = "住所")]
        public string address { get; set; }
        [Display(Name = "最後ログイン")]
        public DateTime lasslogin { get; set; }


    }
}
