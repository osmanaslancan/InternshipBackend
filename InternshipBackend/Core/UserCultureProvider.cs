using Microsoft.AspNetCore.Localization;

namespace InternshipBackend.Core
{
    public class UserCultureProvider : RequestCultureProvider
    {
        public override Task<ProviderCultureResult?> DetermineProviderCultureResult(HttpContext httpContext)
        {
            var culture = httpContext.Request.Cookies["LanguagePreference"];
            if (string.IsNullOrEmpty(culture) ||
                culture.Length != 2)
                return NullProviderCultureResult;

            if (TwoLetterToFourLetter.TryGetValue(culture, out string? code))
                culture = code;
            else
                culture = culture + "-" + culture.ToUpperInvariant();

            return Task.FromResult<ProviderCultureResult?>(new ProviderCultureResult(culture));
        }

        private static readonly Dictionary<string, string> TwoLetterToFourLetter =
            new(StringComparer.OrdinalIgnoreCase)
        {
            { "ar", "ar-SA" },
            { "bn", "bn-BD" },
            { "cs", "cs-CZ" },
            { "da", "da-DK" },
            { "en", "en-US" },
            { "fa", "fa-IR" },
            { "ja", "ja-JP" },
            { "he", "he-IL" },
            { "hi", "hi-IN" },
            { "ko", "ko-KR" },
            { "uk", "uk-UA" },
            { "sv", "sv-SE" },
            { "vi", "vi-VN" },
            { "ur", "ur-PK" },
            { "zh", "zh-CN" },
        };
    }
}
