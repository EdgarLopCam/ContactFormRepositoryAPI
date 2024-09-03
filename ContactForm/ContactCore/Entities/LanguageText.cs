namespace ContactCore.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class LanguageText
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int LanguageId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Key { get; set; } // Ej. "form_title", "submit_button"

        [Required]
        [MaxLength(500)]
        public string Text { get; set; } // Texto traducido correspondiente a la clave

        public Language Language { get; set; }
    }
}
