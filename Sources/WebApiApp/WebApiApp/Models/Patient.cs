using System.ComponentModel.DataAnnotations;

namespace WebApiApp.Models
{
    public class Patient
    {
        [Key]
        public Guid Id { get; set; }
        public string? Use { get; set; }
        [Required]
        public string? Family { get; set; }
        public Gender Gender { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        public bool Active { get; set; }
    }
}