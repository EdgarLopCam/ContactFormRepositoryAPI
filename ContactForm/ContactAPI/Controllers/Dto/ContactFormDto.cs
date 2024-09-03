namespace ContactAPI.Controllers.Dto
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// This DTO is used to transfer data between the client and the server for the contact form functionality.
    /// </summary>
    public class ContactFormDto
    {
        [Required]
        [EmailAddress]
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

        public IFormFile Attachment { get; set; }
    }
}
