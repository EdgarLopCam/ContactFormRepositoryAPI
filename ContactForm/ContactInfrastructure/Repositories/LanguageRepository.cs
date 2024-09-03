namespace ContactInfrastructure.Repositories
{
    using ContactCore.Entities;
    using ContactCore.Interfaces;
    using ContactInfrastructure.Data;
    using Microsoft.EntityFrameworkCore;

    public class LanguageRepository : ILanguageRepository
    {
        private readonly ApplicationDbContext _context;

        public LanguageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Language>> GetAllLanguagesAsync()
        {
            return await _context.Languages.ToListAsync();
        }

        public async Task<IEnumerable<LanguageText>> GetLanguageTextsAsync(int languageId)
        {
            return await _context.LanguageTexts
                .Where(text => text.LanguageId == languageId)
                .ToListAsync();
        }
    }
}
