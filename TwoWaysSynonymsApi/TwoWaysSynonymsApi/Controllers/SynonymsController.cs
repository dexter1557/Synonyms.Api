using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwoWaysSynonyms.Domain;

namespace TwoWaysSynonyms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SynonymsController : ControllerBase
    {
        private readonly SynonymModel synonymModel;

        public SynonymsController(SynonymModel synonymModel)
        {
            this.synonymModel = synonymModel;
        }

        [HttpGet]
        [Route("getitem")]
        public IEnumerable<Synonym> Get()
        {
            return synonymModel.GetSynonyms();
        }
        [HttpPost]
        [Route("saveitem")]
        public void Save(Synonym synonym)
        {
            synonymModel.SaveSynonym(synonym);
        }
    }
}
