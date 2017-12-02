using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusinessGameServer.Controllers
{
    [Produces("application/json")]
    [Route("api/Property")]
    public class PropertyController : Controller
    {
        [HttpGet]
        public IEnumerable<string> GetProp1()
        {
            return new string[] { "value1" };
        }

        [HttpGet("two")]
        public IEnumerable<string> GetProp2()
        {
            return new string[] { "value2" };
        }
    }
}