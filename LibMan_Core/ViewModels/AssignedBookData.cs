namespace LibMan_Core.ViewModels
{
    public class AssignedBookData
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public bool IsAssigned { get; set; }
        public int CopiesOwned { get; set; }
        public int CopiesAvailable { get; set; }
    }
}
