using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookRegisterApi.Models
{
    public class BookRegister
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int userId { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public User User { get; set; }
        public Book Book { get; set; }
    }
}
