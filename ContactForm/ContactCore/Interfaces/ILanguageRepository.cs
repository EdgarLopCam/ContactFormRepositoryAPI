namespace ContactCore.Interfaces
{
    using ContactCore.Entities;

    public interface ILanguageRepository
    {
        Task<IEnumerable<Language>> GetAllLanguagesAsync();
        Task<IEnumerable<LanguageText>> GetLanguageTextsAsync(int languageId);
    }
}
