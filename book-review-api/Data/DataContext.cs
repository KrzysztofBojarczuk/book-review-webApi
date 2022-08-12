using book_review_api.Models;
using Microsoft.EntityFrameworkCore;

namespace book_review_api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
    }
}
