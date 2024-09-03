namespace ContactAPI.Controllers.v1
{
    using ContactCore.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Provides endpoints to retrieve available languages and their associated texts.
    /// </summary>

    [ApiController]
    [Route("api/v1/[controller]")]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageRepository _languageRepository;

        public LanguageController(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        /// <summary>
        /// Retrieves all available languages.
        /// </summary>
        /// <returns>A list of languages available in the system.</returns>
        [HttpGet]
        public async Task<IActionResult> GetLanguages()
        {
            var languages = await _languageRepository.GetAllLanguagesAsync();
            return Ok(languages);
        }

        /// <summary>
        /// Retrieves the texts associated with a specific language.
        /// </summary>
        /// <param name="languageId">The unique identifier of the language.</param>
        /// <returns>A list of texts corresponding to the specified language.</returns>
        [HttpGet("{languageId}/texts")]
        public async Task<IActionResult> GetLanguageTexts(int languageId)
        {
            var texts = await _languageRepository.GetLanguageTextsAsync(languageId);
            return Ok(texts);
        }
    }
}
