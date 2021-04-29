using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RobberLanguageAPI.Data;
using RobberLanguageAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobberLanguageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranslationController : ControllerBase
    {
        private readonly RobberTranslationDbContext _context;
        private readonly Translation translation = new Translation();

        public TranslationController(RobberTranslationDbContext context)
        {
            _context = context;
        }

        [Route("CreateTranslation")]
        [HttpPost]
        public async Task<IActionResult> PostTranslateSentence(Translation obj)
        {
            if (String.IsNullOrWhiteSpace(obj.OriginalSentence))
            {
                return BadRequest();
            }
            translation.OriginalSentence = obj.OriginalSentence;
            translation.TranslatedSentence = TranslateSentence(obj.OriginalSentence);

            try
            {
                await _context.Translations.AddAsync(translation);
                await _context.SaveChangesAsync();
                return new CreatedAtActionResult(nameof(PostTranslateSentence), "Translation", new { id = translation.Id }, translation);
            }
            catch
            {
                return BadRequest();
            }
        }

        private static string TranslateSentence(string sentence)
        {
            var letters = sentence.ToCharArray();
            var partsOfSentence = new List<string>();

            foreach (var letter in letters)
            {
                partsOfSentence.Add(letter.ToString());
            }

            for(int i = 0; i < partsOfSentence.Count; i++)
            {
                if(isConsonant(partsOfSentence[i]))
                {
                    partsOfSentence[i] = $"{partsOfSentence[i]}o{partsOfSentence[i]}";
                }
            }

            string newSentence = "";

            for(int i = 0; i < partsOfSentence.Count; i++)
            {
                newSentence += partsOfSentence[i];
            }

            return newSentence;
        }

        private static bool isConsonant(string letter)
        {
            letter.ToLower();
            var consonants = new List<string> { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "z" };

            foreach (var consonant in consonants)
            {
                if (consonant == letter) 
                {
                    return true;
                }
            }
            return false;
        }
    }
}
