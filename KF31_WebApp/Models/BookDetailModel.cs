namespace KF31_WebApp.Models
{
    public class BookDetailModel
    {
        public Book Book { get; set; }
        public List<Stock> Stock { get; set; }
        public int Total {  get; set; }
    }
}
