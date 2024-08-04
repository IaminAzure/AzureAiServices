using az.aiservice.helpers.Interface;
using az.aiservice.helpers.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace azure.aiservices.Controllers
{
    [ApiController]
    public class AILanguageController : ControllerBase
    {
        private readonly ILogger<AILanguageController> _logger;
        private readonly IAzLanguageHelper _languageHelper;

        public AILanguageController(IAzLanguageHelper languageHelper,ILogger<AILanguageController> logger )
        {
            _languageHelper = languageHelper;
            _logger = logger;
        }

        [HttpPost]
        [Route("DetectLanguage")]
        public async Task<AiServiceResponse> DetectLanguage(string prompt)
        {
            try
            {
                var language =await  _languageHelper.LanguageDetection(prompt);
                return new AiServiceResponse() { 
                ResponseStatus = HttpStatusCode.OK,
                Response = language.Name
                };
            }
            catch (Exception ex) {
                return new AiServiceResponse()
                {
                    ResponseStatus = HttpStatusCode.BadRequest,
                    Response = ex.Message
                };
            }

        }

        [HttpPost]
        [Route("LanguageTranslate")]
        public async Task<AiServiceResponse> LanguageTranslate(string prompt,string targetLanguage)
        {
            try
            {
                var translatedPrompt = await _languageHelper.LanguageTranslate(prompt,targetLanguage);
                return new AiServiceResponse()
                {
                    ResponseStatus = HttpStatusCode.OK,
                    Response = translatedPrompt
                };
            }
            catch (Exception ex)
            {
                return new AiServiceResponse()
                {
                    ResponseStatus = HttpStatusCode.BadRequest,
                    Response = ex.Message
                };
            }

        }



    }
}
