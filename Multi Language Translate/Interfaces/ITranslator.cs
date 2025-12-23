namespace Multi_Language_Translate.Interfaces
{
    /// <summary>
    /// Translator interface for dependency injection
    /// </summary>
    public interface ITranslator
    {
        /// <summary>
        /// Translates text from source language to target language asynchronously
        /// </summary>
        /// <param name="text">Text to translate</param>
        /// <param name="sourceLanguage">Source language code (e.g., "en", "tr")</param>
        /// <param name="targetLanguage">Target language code (e.g., "fr", "de")</param>
        /// <returns>Translated text</returns>
        Task<string> TranslateAsync(string text, string sourceLanguage, string targetLanguage);

        /// <summary>
        /// Gets list of supported languages
        /// </summary>
        /// <returns>Dictionary of language codes and names</returns>
        Task<Dictionary<string, string>> GetSupportedLanguagesAsync();
    }
}
