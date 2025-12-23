using System.Net.Http.Json;
using System.Text.Json;
using System.Web;
using Multi_Language_Translate.Interfaces;

namespace Multi_Language_Translate.Services
{
    /// <summary>
    /// Translation service using Google Translate API (free endpoint)
    /// </summary>
    public class TranslationService : ITranslator
    {
        private readonly HttpClient _httpClient;
        private const string GoogleTranslateUrl = "https://translate.googleapis.com/translate_a/element.js";

        public TranslationService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            // Set a realistic user agent to avoid being blocked
            if (!_httpClient.DefaultRequestHeaders.Contains("User-Agent"))
            {
                _httpClient.DefaultRequestHeaders.Add("User-Agent", 
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36");
            }
        }

        /// <summary>
        /// Translates text using Google Translate API
        /// </summary>
        public async Task<string> TranslateAsync(string text, string sourceLanguage, string targetLanguage)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Text cannot be empty", nameof(text));

            if (string.IsNullOrWhiteSpace(sourceLanguage))
                throw new ArgumentException("Source language cannot be empty", nameof(sourceLanguage));

            if (string.IsNullOrWhiteSpace(targetLanguage))
                throw new ArgumentException("Target language cannot be empty", nameof(targetLanguage));

            try
            {
                // Use alternative Google Translate endpoint
                string url = $"https://translate.googleapis.com/translate_a/single?" +
                    $"client=gtx&" +
                    $"sl={Uri.EscapeDataString(sourceLanguage)}&" +
                    $"tl={Uri.EscapeDataString(targetLanguage)}&" +
                    $"dt=t&" +
                    $"q={Uri.EscapeDataString(text)}";

                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Translation API error: {response.StatusCode}");
                }

                var content = await response.Content.ReadAsStringAsync();
                
                // Parse the response - Google returns JSON array format
                // Response format: [[[translated_text, original_text, ...],...],...]
                var translatedText = ParseGoogleTranslateResponse(content);
                
                if (string.IsNullOrEmpty(translatedText))
                {
                    throw new InvalidOperationException("No translation result received");
                }

                return translatedText;
            }
            catch (HttpRequestException ex)
            {
                throw new InvalidOperationException($"Translation service error: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Unexpected error during translation: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Parses Google Translate API response
        /// </summary>
        private static string ParseGoogleTranslateResponse(string jsonResponse)
        {
            try
            {
                // Google returns: [[[translated_text,original_text,...],[],...],null,original_language,...]
                using var doc = JsonDocument.Parse(jsonResponse);
                var root = doc.RootElement;

                if (root.ValueKind == JsonValueKind.Array && root.GetArrayLength() > 0)
                {
                    var firstElement = root[0];
                    if (firstElement.ValueKind == JsonValueKind.Array && firstElement.GetArrayLength() > 0)
                    {
                        var translationArray = firstElement[0];
                        if (translationArray.ValueKind == JsonValueKind.Array && 
                            translationArray.GetArrayLength() > 0)
                        {
                            var translated = translationArray[0];
                            if (translated.ValueKind == JsonValueKind.String)
                            {
                                return translated.GetString() ?? "";
                            }
                        }
                    }
                }

                return "";
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Gets supported languages 
        /// </summary>
        public async Task<Dictionary<string, string>> GetSupportedLanguagesAsync()
        {
            return await Task.FromResult(GetDefaultLanguages());
        }

        /// <summary>
        /// Returns a set of commonly used languages supported by Google Translate
        /// </summary>
        private static Dictionary<string, string> GetDefaultLanguages()
        {
            return new Dictionary<string, string>
            {
                { "af", "Afrikaans" },
                { "ar", "Arabic" },
                { "bg", "Bulgarian" },
                { "bn", "Bengali" },
                { "ca", "Catalan" },
                { "cs", "Czech" },
                { "cy", "Welsh" },
                { "da", "Danish" },
                { "de", "German" },
                { "el", "Greek" },
                { "en", "English" },
                { "es", "Spanish" },
                { "et", "Estonian" },
                { "fa", "Persian" },
                { "fi", "Finnish" },
                { "fr", "French" },
                { "gu", "Gujarati" },
                { "he", "Hebrew" },
                { "hi", "Hindi" },
                { "hr", "Croatian" },
                { "hu", "Hungarian" },
                { "id", "Indonesian" },
                { "it", "Italian" },
                { "ja", "Japanese" },
                { "kn", "Kannada" },
                { "ko", "Korean" },
                { "lt", "Lithuanian" },
                { "lv", "Latvian" },
                { "mk", "Macedonian" },
                { "ml", "Malayalam" },
                { "mr", "Marathi" },
                { "ne", "Nepali" },
                { "nl", "Dutch" },
                { "pa", "Punjabi" },
                { "pl", "Polish" },
                { "pt", "Portuguese" },
                { "ro", "Romanian" },
                { "ru", "Russian" },
                { "sk", "Slovak" },
                { "sl", "Slovenian" },
                { "so", "Somali" },
                { "sq", "Albanian" },
                { "sv", "Swedish" },
                { "ta", "Tamil" },
                { "te", "Telugu" },
                { "th", "Thai" },
                { "tl", "Filipino" },
                { "tr", "Turkish" },
                { "uk", "Ukrainian" },
                { "ur", "Urdu" },
                { "vi", "Vietnamese" },
                { "zh-CN", "Chinese (Simplified)" },
                { "zh-TW", "Chinese (Traditional)" },
                { "zu", "Zulu" }
            };
        }
    }
}
