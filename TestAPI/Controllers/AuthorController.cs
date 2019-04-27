using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI.Models;
using Newtonsoft.Json;
using System.Web.Http;

namespace TestAPI.Controllers
{

    [ApiVersion("1.0")]
    [Route("api/author")]
    [ApiController]
    public class AuthorV1Controller : ControllerBase
    {
        pubsContext pubs = new pubsContext();
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetAuthor(string name)
        {
            var author = pubs.Authors.Where(x => x.AuFname.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();
            return Ok(author);
            //throw new NotImplementedException();
        }
    }

    [ApiVersion("2.0")]
    [Route("api/author")]
    [ApiController]
    public class Authorv2Controller : ControllerBase
    {
        pubsContext pubs = new pubsContext();
        [HttpGet]
        [ProducesResponseType(202)]
        public IActionResult GetAuthor(string name)
        {
            var author = pubs.Authors.Where(x => x.AuFname.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();
            return Accepted(author);
            //throw new NotImplementedException();
        }
    }
}
