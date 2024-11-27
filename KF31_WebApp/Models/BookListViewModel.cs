using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace KF31_WebApp.Models

{
    public class BookListViewModel
    {
        public IEnumerable<Book> Books { get; set; } // Bookリスト
        public int CurrentPage { get; set; } // 現在のページ
        public int TotalPages { get; set; } // ページ数
        public int keyword {  get; set; }
    }
}
