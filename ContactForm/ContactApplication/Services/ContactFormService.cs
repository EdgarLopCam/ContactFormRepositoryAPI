namespace ContactApplication.Services
{
    using ContactCore.Entities;
    using ContactCore.Interfaces;
    using Microsoft.Extensions.Logging;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Service class for managing contact form operations.
    /// Provides methods to handle CRUD operations and validation for contact forms.
    /// </summary>
    public class ContactFormService : IContactFormService
    {
        private readonly IContactFormRepository _repository;
        private readonly ILogger<ContactFormService> _logger;

        public ContactFormService(IContactFormRepository repository, ILogger<ContactFormService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all contact form entries from the repository.
        /// </summary>
        /// <returns>An enumerable list of contact forms.</returns>
        public async Task<IEnumerable<ContactForm>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        /// <summary>
        /// Retrieves a specific contact form entry by its unique ID.
        /// </summary>
        /// <param name="id">The unique identifier of the contact form.</param>
        /// <returns>The contact form with the specified ID.</returns>
        public async Task<ContactForm> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        /// <summary>
        /// Adds a new contact form entry to the repository after validation.
        /// </summary>
        /// <param name="contactForm">The contact form to be added.</param>
        public async Task AddAsync(ContactForm contactForm)
        {
            ValidateContactForm(contactForm);
            await _repository.AddAsync(contactForm);
        }

        /// <summary>
        /// Validates the fields of a contact form.
        /// Checks for valid email, first name, last name, comments, and attachment type.
        /// Throws an exception if validation fails.
        /// </summary>
        /// <param name="contactForm">The contact form to validate.</param>
        /// <exception cref="ArgumentException">Thrown when validation fails.</exception>
        private void ValidateContactForm(ContactForm contactForm)
        {
            if (string.IsNullOrWhiteSpace(contactForm.Email) || !Regex.IsMatch(contactForm.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("Invalid or missing email address.");

            if (string.IsNullOrWhiteSpace(contactForm.FirstName) || !Regex.IsMatch(contactForm.FirstName, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$"))
                throw new ArgumentException("First name contains invalid characters or is missing.");

            if (string.IsNullOrWhiteSpace(contactForm.LastName) || !Regex.IsMatch(contactForm.LastName, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$"))
                throw new ArgumentException("Last name contains invalid characters or is missing.");

            if (string.IsNullOrWhiteSpace(contactForm.Comments) || !Regex.IsMatch(contactForm.Comments, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$"))
                throw new ArgumentException("Comments contain invalid characters or are missing.");

            // Validar el tipo de archivo adjunto si existe
            if (contactForm.AttachmentData != null)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf" };
                var extension = System.IO.Path.GetExtension(contactForm.AttachmentFileName).ToLower();
                if (!Array.Exists(allowedExtensions, e => e == extension))
                {
                    throw new ArgumentException("Attachment must be an image or PDF.");
                }
            }
        }
    }
}
