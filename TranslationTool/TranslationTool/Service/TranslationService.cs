using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using TranslatorTool.Services.Interfaces;
using GoogleTranslateFreeApi;
using System.Threading.Tasks;

namespace TranslatorTool.Services
{
   public class TranslationService : ITranslationService
   {
      private readonly IWebHostEnvironment _hostingEnvironment;

      public TranslationService(IWebHostEnvironment hostingEnvironment)
      {
         _hostingEnvironment = hostingEnvironment;
      }

      public async Task<string> Translate(string textToTranslate)
      {
        GoogleTranslator googleTranslator = new GoogleTranslator();
        TranslationResult result = await googleTranslator.TranslateAsync(textToTranslate, Language.Auto, Language.English);

        string translatedText = result.MergedTranslation;

        SaveTranslation(textToTranslate, translatedText);
         
        return translatedText;
      }

      public string TranslationExists(string text)
      {
         string path = Path.Combine(_hostingEnvironment.WebRootPath, "Translations.xml");
         try
         {
            XDocument document = XDocument.Load(path);

            string result = document.Descendants("translation")
                .FirstOrDefault(p => p.Elements("from")
                  .Any(c => c.Value.Equals(text))
                )?.Element("to").Value;
            return result;
         }
         catch (FileNotFoundException)
         {
            throw new FileNotFoundException();
         }
      }

      public void SaveTranslation(string textForTranslate, string translatedText)
      {
         string path = Path.Combine(_hostingEnvironment.WebRootPath, "Translations.xml");

         XElement newTranslation = new XElement("translation",
                           new XElement("from", textForTranslate),
                           new XElement("to", translatedText));

         newTranslation.SetAttributeValue("timestamp", DateTime.Now.ToString());

         if (!File.Exists(path))
         {
            newTranslation.SetAttributeValue("id", 1);
            new XDocument(new XElement("translations", newTranslation)).Save(path);
         }
         else
         {
            XDocument document = XDocument.Load(path);
            int count = document.Descendants("translation").Count();
            newTranslation.SetAttributeValue("id", count + 1);
            document.Root.Add(newTranslation);
            document.Save(path);
         }
      }

   }
}
