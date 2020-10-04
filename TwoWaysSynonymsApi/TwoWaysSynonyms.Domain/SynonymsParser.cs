using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace TwoWaysSynonyms.Domain
{
    public class SynonymsParser
    {
        public IEnumerable<TermWithSynonyms> AssignSynonymsToTerms(IEnumerable<Synonym> synonyms)
        {
            _ = synonyms ?? throw new ArgumentNullException($"{nameof(synonyms)} cannot be null");

            var synonymsDecoupled = synonyms.
                Select(x => new { x.Term, SynonymsTable = x.Synonyms.Split(',') })
                .SelectMany(x => x.SynonymsTable, (termKey, synonymValue) => new { termKey.Term, synonymValue })
                .Distinct();

            return synonymsDecoupled.ToLookup(x => x.Term, x => x.synonymValue).Select(x => new TermWithSynonyms() { Term = x.Key, Synonyms = x.ToArray() }); ;
        }

        public IEnumerable<Synonym> GetAllSynonymObjectsFromSynonym(Synonym synonym)
        {
            _ = synonym ?? throw new ArgumentNullException($"{nameof(synonym)} cannot be null");
            List<Synonym> synonyms = new List<Synonym>();
            synonyms.Add(synonym);
            synonyms.AddRange(synonym.Synonyms.Split(',').Select(x => new Synonym { Term = x, Synonyms = synonym.Term }).ToList());
            return synonyms;
        }

        public Synonym ConvertTermWithSynonymToSynonym(TermWithSynonyms termWithSynonyms)
        {
            _ = termWithSynonyms ?? throw new ArgumentNullException($"{nameof(termWithSynonyms)} cannot be null");
            return new Synonym { Term = termWithSynonyms.Term, Synonyms = string.Join(",", termWithSynonyms.Synonyms) };
        }
    }
}
