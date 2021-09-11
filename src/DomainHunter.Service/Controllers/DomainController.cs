using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainHunter.BLL;
using Microsoft.AspNetCore.Mvc;

namespace DomainHunter.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DomainController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Domain>> Get()
        {
            return new List<Domain>();
        }
    }
}
