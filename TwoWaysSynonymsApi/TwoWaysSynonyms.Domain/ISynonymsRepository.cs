using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwoWaysSynonyms.Domain
{
   public interface ISynonymsRepository :IRepository<Synonym>
    {
        IEnumerable<Synonym> GetAllSynonyms();
        void SaveSynonym(Synonym synonym);
    }
}
