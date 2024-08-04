using Azure.AI.TextAnalytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace az.aiservice.helpers.Interface
{
    public interface IAzLanguageHelper
    {
        Task<DetectedLanguage> LanguageDetection(string prompt);
        Task<string> LanguageTranslate(string prompt, string toLanguage);
    }
}
