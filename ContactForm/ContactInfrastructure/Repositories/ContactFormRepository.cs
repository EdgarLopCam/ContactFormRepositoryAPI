namespace ContactInfrastructure.Repositories
{
    using ContactCore.Entities;
    using ContactCore.Interfaces;
    using ContactInfrastructure.Data;
    using Microsoft.EntityFrameworkCore;

    public class ContactFormRepository : IContactFormRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactFormRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContactForm>> GetAllAsync()
        {
            return await _context.ContactForms.ToListAsync();
        }

        public async Task<ContactForm> GetByIdAsync(int id)
        {
            return await _context.ContactForms.FindAsync(id);
        }

        public async Task AddAsync(ContactForm contactForm)
        {
            await _context.ContactForms.AddAsync(contactForm);
            await _context.SaveChangesAsync();
        }
    }
}
