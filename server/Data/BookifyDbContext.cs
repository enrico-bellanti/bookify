using Bookify.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Data
{
    public class BookifyDbContext: DbContext
    {
        public BookifyDbContext(DbContextOptions<BookifyDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Accommodation> Accommodations { get; set; }

    }
}
