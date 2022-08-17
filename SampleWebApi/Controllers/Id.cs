using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleWebApi.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SampleWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdController : ControllerBase
    {
        [HttpGet(Name = "Id")]
        // GET: /Id
        public IEnumerable<string> Get()
        {
            return new string[] { "Hello World", "I am a Sample Controller" };
        }
    }
}

