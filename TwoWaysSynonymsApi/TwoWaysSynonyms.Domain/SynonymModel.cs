using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace TwoWaysSynonyms.Domain
{
    public class SynonymModel
    {
        private readonly ISynonymsRepository repository;
        private readonly SynonymsParser parser;

        public SynonymModel(ISynonymsRepository repository, SynonymsParser parser)
        {
            this.repository = repository;
            this.parser = parser;
        }

        public IEnumerable<Synonym> GetSynonyms()
        {
            var synonymsList = repository.GetAllSynonyms();
            var tmp = parser.AssignSynonymsToTerms(synonymsList);

            return tmp.Select(x => parser.ConvertTermWithSynonymToSynonym(x));
        }
        public void SaveSynonym(Synonym synonym)
        {
            var synonymsToSave = parser.GetAllSynonymObjectsFromSynonym(synonym);
            foreach (var synonymItem in synonymsToSave)
            {
                repository.SaveSynonym(synonymItem);
            };
        }
    }
}
