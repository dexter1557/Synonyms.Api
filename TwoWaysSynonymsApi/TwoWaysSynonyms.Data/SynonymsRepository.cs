using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwoWaysSynonyms.Domain;

namespace TwoWaysSynonyms.Data
{
    public class SynonymsContext : DbContext 
    {
        public SynonymsContext(DbContextOptions options):
            base(options)
        {

        }
        public DbSet<Synonym> Synonyms { get; set; }
    }
    public class SynonymsRepository : ISynonymsRepository
    {
        private readonly SynonymsContext synonymsContext;

        public SynonymsRepository(SynonymsContext synonymsContext)
        {
            this.synonymsContext = synonymsContext;
        }

        public IEnumerable<Synonym> GetAllSynonyms()
        {
            return GetAllSynonymsAsync().Result;
        }
        private async Task<IEnumerable<Synonym>> GetAllSynonymsAsync()
        {
            return await synonymsContext.Synonyms.ToListAsync();
        }

        public void SaveSynonym(Synonym synonym)
        {
           SaveSynonymAsync(synonym);
        }
        private void  SaveSynonymAsync(Synonym synonym)
        {
            synonymsContext.Synonyms.Add(synonym);
            synonymsContext.SaveChanges();
        }
    }
}
