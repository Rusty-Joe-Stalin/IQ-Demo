using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Policy;
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace DemoProject.Controllers
{
    [ApiController]
    [Route("/")]
    public class DemoController : ControllerBase
    {
        private readonly ILogger<DemoController> _logger;

        public DemoController(ILogger<DemoController> logger)
        {
            _logger = logger;
        }
                
        [HttpGet("ping")]
        public String GetAssembly()
        {
            return GetType().Assembly.GetName().Version.ToString();
        }

        [HttpGet("dogs")]
        public RedirectResult getDogPicAsync()
        {
            var client = new RestClient("https://dog.ceo/api/breeds/image/random");

            var request = new RestRequest();

            request.AddParameter("Content-Type", "application/json");

            var response = client.ExecuteAsGet(request,"get");

            var result = JsonConvert.DeserializeObject<Result>(response.Content); 

           return Redirect(result.message);

        }
    }
}
