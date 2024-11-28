using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace KF31_WebApp.Models
{
    [Table("Book_table")]
    public class Book
    {

        [Key]
        [Display(Name = "本コード")]
        public string BookID { get; set; }

        [Display(Name = "タイトル")]
        public string Book_title { get; set; }
        [Display(Name = "カテゴリ")]
        public string CategoryID { get; set; }
        [Display(Name = "著者")]
        public string Book_Author { get; set; }
        [Display(Name = "出版社")]
        public string PublisherID { get; set; }

        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }
        [ForeignKey("PublisherID")]
        public virtual Publisher Publisher { get; set; }

    }
}
