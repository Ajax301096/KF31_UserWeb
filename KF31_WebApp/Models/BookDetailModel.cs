namespace KF31_WebApp.Models
{
    public class BookDetailModel
    {
        public string? userID {  get; set; }
        public Book Book { get; set; }
        public List<Stock> Stock { get; set; }
        public List<Libraty> Libraty { get; set; } = new List<Libraty>();
        public int Total {  get; set; }
    }
}
