using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TranslatorTool.Services.Interfaces;

namespace TranslatorTool.Controllers
{
   public class TranslationController : Controller
   {
      private ITranslationService _translationService;

      public TranslationController(ITranslationService TranslationService)
      {
         _translationService = TranslationService;
      }
      [Route("/api/translate")]
      [HttpPost]
      public async Task<IActionResult> Translation([FromBody] string text)
      {
         if (String.IsNullOrEmpty(text) || String.IsNullOrWhiteSpace(text))
            return BadRequest();

         string translatedText = _translationService.TranslationExists(text);

         if (String.IsNullOrEmpty(translatedText))
         {
           translatedText = await _translationService.Translate(text);
         }
         return Json(translatedText);
      }
   }
}