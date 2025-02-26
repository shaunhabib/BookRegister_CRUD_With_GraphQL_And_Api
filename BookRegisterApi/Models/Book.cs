using System.ComponentModel.DataAnnotations;

namespace BookRegisterApi.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
