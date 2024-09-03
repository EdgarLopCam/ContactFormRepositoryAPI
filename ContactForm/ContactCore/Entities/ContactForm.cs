namespace ContactCore.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class ContactForm
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(500)]
        public string Comments { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public byte[] AttachmentData { get; set; } // Contenido del archivo adjunto
        public string AttachmentFileName { get; set; } // Nombre del archivo adjunto para referencia
    }
}
