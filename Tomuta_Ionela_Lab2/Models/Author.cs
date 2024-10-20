using System.ComponentModel.DataAnnotations;

namespace Tomuta_Ionela_Lab2.Models
{
    public class Author
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}