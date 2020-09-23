using System.Threading.Tasks;
using TranslatorTool.Models;

namespace TranslatorTool.Services.Interfaces
{
   public interface ITranslationService
   {
      Task<string> Translate(string text);
      string TranslationExists(string text);
      void SaveTranslation(string fromText, string toText);
   }
}