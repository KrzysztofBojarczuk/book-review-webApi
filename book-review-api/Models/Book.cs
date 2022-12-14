namespace book_review_api.Models
{
    public class Book 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<BookOwner> BookOwners { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; }
    }
}
