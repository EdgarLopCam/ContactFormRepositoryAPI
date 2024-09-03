namespace ContactCore.Interfaces
{
    using ContactCore.Entities;

    public interface IContactFormRepository
    {
        Task<IEnumerable<ContactForm>> GetAllAsync();
        Task<ContactForm> GetByIdAsync(int id);
        Task AddAsync(ContactForm contactForm);

    }
}
