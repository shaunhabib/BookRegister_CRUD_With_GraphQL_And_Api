using BookRegisterApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookRegisterApi.DataContext
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BookRegister> BookRegisters { get; set; }


    }
}
