using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace KF31_WebApp.Models
{
    public class YoyakuListViewModel
    {
        public string? userID { get; set; }
        public IEnumerable<Yoyaku> Yoyaku { get; set; } // 予約リスト

        [ForeignKey("userID")]
        public virtual Member Member { get; set; }
        public int CurrentPage { get; set; } // 現在のページ
        public int TotalPages { get; set; } // ページ数
    }
}
