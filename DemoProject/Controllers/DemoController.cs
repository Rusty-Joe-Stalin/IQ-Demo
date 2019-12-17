using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;
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
        public RedirectResult GetDogPicAsync()
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
