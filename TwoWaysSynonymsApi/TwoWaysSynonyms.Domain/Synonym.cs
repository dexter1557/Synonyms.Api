using System;
using System.Collections.Generic;
using System.Text;

namespace TwoWaysSynonyms.Domain
{
    public class Synonym
    {
        public int Id { get; set; }
        public string Term { get; set; }
        public string Synonyms { get; set; }
    }
}
