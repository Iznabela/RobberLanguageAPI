using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobberLanguageAPI.Models
{
    public class Translation
    {
        public int Id { get; set; }
        public string OriginalSentence { get; set; }
        public string TranslatedSentence { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
