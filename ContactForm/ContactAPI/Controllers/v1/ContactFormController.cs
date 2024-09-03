namespace ContactAPI.Controllers.v1
{
    using ContactAPI.Controllers.Dto;
    using ContactCore.Entities;
    using ContactCore.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Provides endpoints to create a new contact form entry and retrieve an existing one by ID.
    /// </summary>

    [ApiController]
    [Route("api/v1/[controller]")]
    public class ContactFormController : ControllerBase
    {
        private readonly IContactFormService _service;

        public ContactFormController(IContactFormService service)
        {
            _service = service;
        }

        /// <summary>
        /// Creates a new contact form entry in the database.
        /// Accepts a contact form submission with optional file attachment.
        /// </summary>
        /// <param name="contactFormDto">The data transfer object containing the contact form information.</param>
        /// <returns>A status message indicating the result of the operation.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ContactFormDto contactFormDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var contactForm = new ContactForm
            {
                Email = contactFormDto.Email,
                FirstName = contactFormDto.FirstName,
                LastName = contactFormDto.LastName,
                Comments = contactFormDto.Comments
            };

            if (contactFormDto.Attachment != null)
            {
                using (var ms = new MemoryStream())
                {
                    await contactFormDto.Attachment.CopyToAsync(ms);
                    contactForm.AttachmentData = ms.ToArray();
                    contactForm.AttachmentFileName = contactFormDto.Attachment.FileName;
                }
            }

            await _service.AddAsync(contactForm);
            return Ok(new { Message = "Form submitted successfully!" });
        }

        /// <summary>
        /// Retrieves an existing contact form entry by its unique ID.
        /// </summary>
        /// <param name="id">The unique identifier of the contact form.</param>
        /// <returns>The contact form data if found; otherwise, a 404 Not Found status.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var contactForm = await _service.GetByIdAsync(id);
            if (contactForm == null)
                return NotFound();

            return Ok(contactForm);
        }
    }
}
