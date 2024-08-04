using az.aiservice.helpers.Interface;
using az.aiservice.helpers.Model;
using Azure;
using Azure.AI.TextAnalytics;
using Azure.AI.Translation.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace az.aiservice.helpers.Implementation
{
    public class AzLanguageHelper : IAzLanguageHelper
    {
        private readonly AppSettings _azureAppSettings;

        public AzLanguageHelper(IOptions<AppSettings> configuration )
        {
            this._azureAppSettings = configuration.Value;
        }

        public async Task<Azure.AI.TextAnalytics.DetectedLanguage> LanguageDetection(string prompt)
        {
            AzureKeyCredential creds = new AzureKeyCredential(_azureAppSettings.languageSubscriptionKey);
            Uri endpoint = new Uri(_azureAppSettings.languageUrl);
            var client = new TextAnalyticsClient(endpoint, creds);
            var response=await  client.DetectLanguageAsync(prompt);
            return response.Value;

        }

        public async Task<string> LanguageTranslate(string prompt,string toLanguage)
        {
            AzureKeyCredential creds = new AzureKeyCredential(_azureAppSettings.translateSubscriptionKey);
            Uri endpoint = new Uri(_azureAppSettings.translateUrl);
            var client = new TextTranslationClient(creds, endpoint);
            var sourceLanguage=await  LanguageDetection(prompt);
            var response = await client.TranslateAsync(toLanguage, prompt, sourceLanguage: sourceLanguage.Iso6391Name);
            return response?.Value?.FirstOrDefault()?.Translations?.FirstOrDefault()?.Text;

        }
    }
}
