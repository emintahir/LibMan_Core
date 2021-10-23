namespace LibMan_Core.Models
{
    public class BookLend
    {
        public int LendId { get; set; }
        public Lend Lend { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
