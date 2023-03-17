using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;

namespace WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LicenseHandlerController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<LicenseHandler> _logger;


        public LicenseHandlerController(IWebHostEnvironment webHostEnvironment, ILogger<LicenseHandler> logger)
        {
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        [HttpGet(Name = "LicenseHandler")]
        public LicenseHandler Get()
        {
            string contentRootPath = _webHostEnvironment.ContentRootPath;

            string sFileJson = Path.Combine(contentRootPath, "LicenseFile", "license-config.json");
            if (sFileJson == null || sFileJson.Length == 0 || !System.IO.File.Exists(sFileJson))
            {
                NotFound();
            }

            LicenseHandler item = null;
            using (StreamReader r = new StreamReader(sFileJson))
            {
                string json = r.ReadToEnd();
                item = JsonConvert.DeserializeObject<LicenseHandler>(json);
                //List<LicenseHandler> items = JsonConvert.DeserializeObject<List<LicenseHandler>>(json);
            }
            
            return item;
        }

        [HttpPut(Name = "LicenseHandler")]
        public LicenseHandler Update(string LicenseNumber, string ClientName)
        {
            string contentRootPath = _webHostEnvironment.ContentRootPath;

            string sFileJson = Path.Combine(contentRootPath, "LicenseFile", "license-config.json");
            if (sFileJson == null || sFileJson.Length == 0 || !System.IO.File.Exists(sFileJson))
            {
                NotFound();
            }

            LicenseHandler item = null;
            using (StreamReader r = new StreamReader(sFileJson))
            {
                string json = r.ReadToEnd();
                item = JsonConvert.DeserializeObject<LicenseHandler>(json);
                //List<LicenseHandler> items = JsonConvert.DeserializeObject<List<LicenseHandler>>(json);
            }

            return item;
        }
    }
}
