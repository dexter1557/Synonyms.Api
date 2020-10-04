using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace TwoWaysSynonyms.Domain.Tests
{

    [TestClass]
    public class SynonymsParserTest
    {
        private SynonymsParser _synonymsParser;
        
        public SynonymsParserTest()
        {
            _synonymsParser = new SynonymsParser();
        }

        [TestMethod]
        public void ParserShouldFilterOutDuplactedSynonyms()
        {
            var data = new List<Synonym>
            {
                new Synonym{Term="computer", Synonyms="laptop,notebook"},
                new Synonym{Term="computer", Synonyms="PC,notebook"},
            };
            var expectedResult = new List<TermWithSynonyms>
            {
                new TermWithSynonyms{
                    Term="computer",
                    Synonyms=new string[]{"laptop","notebook","PC"}
                },
            };
            var actualResult = _synonymsParser.AssignSynonymsToTerms(data).ToList();
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ParserShouldReturnAmountOfObjectsEqualToAmountOfDifferentTerms()
        {
            var data = new List<Synonym>
            {
                new Synonym{Term="computer", Synonyms="laptop,notebook"},
                new Synonym{Term="computer", Synonyms="PC,notebook"},
                new Synonym{Term="laptop", Synonyms="computer"},
            };
            var expectedResult = 2;
            var actualResult = _synonymsParser.AssignSynonymsToTerms(data).ToList().Count;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ExpceptionShouldBeThrownWhenNullIsPassed()
        {
            Assert.ThrowsException<ArgumentNullException>(()=>_synonymsParser.AssignSynonymsToTerms(null));
            Assert.ThrowsException<ArgumentNullException>(() => _synonymsParser.GetAllSynonymObjectsFromSynonym(null));
        }

        [TestMethod]
        public void TermWithSynonymsShouldCreateAsManySynonymsAsInSynonymsArrayPlusItself()
        {
            var data = new Synonym
            {
                Term = "computer",
                Synonyms = "laptop,notebook,PC"
            };
            var expectedSynonyms = 4;
            var actualSynonyms = _synonymsParser.GetAllSynonymObjectsFromSynonym(data).Count();
            Assert.AreEqual(expectedSynonyms, actualSynonyms);
        }

        [TestMethod]
        public void FirstSynonymShouldContainAllTheSynonymsSeparatedByComa()
        {
            var data = new Synonym
            {
                Term = "computer",
                Synonyms = "laptop,notebook,PC"
            };
            var expectedSynonymsString="laptop,notebook,PC";
            var actualSynonymsData = _synonymsParser.GetAllSynonymObjectsFromSynonym(data).First().Synonyms;
            Assert.AreEqual(expectedSynonymsString, actualSynonymsData);
        }
    }
}
