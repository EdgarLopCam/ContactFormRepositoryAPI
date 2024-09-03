namespace ContactCore.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Language
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Code { get; set; } // Ej. "en", "es"

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } // Ej. "English", "Español"
    }
}
