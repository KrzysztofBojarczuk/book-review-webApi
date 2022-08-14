namespace book_review_api.Dto.ReviewDto
{
    public class ReviewCreateDto
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
    }
}
