using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.IO;
using System.Net.Mime;

namespace WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LicenseHandlerController : Controller
    {
        private const string licenseFileName = "license-config.json";
        private const string licenseFileFolder = "LicenseFile";
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

            string sFileJson = Path.Combine(contentRootPath, licenseFileFolder, licenseFileName);
            if (sFileJson == null || sFileJson.Length == 0 || !System.IO.File.Exists(sFileJson))
            {
                NotFound();
            }

            LicenseHandler item;
            using (StreamReader r = new StreamReader(sFileJson))
            {
                string json = r.ReadToEnd();
                item = JsonConvert.DeserializeObject<LicenseHandler>(json);
                //List<LicenseHandler> items = JsonConvert.DeserializeObject<List<LicenseHandler>>(json);
            }
            
            return item;
        }

        [HttpPost(Name = "LicenseHandler")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public JsonResult Post(LicenseHandler licData)
        {
            string contentRootPath = _webHostEnvironment.ContentRootPath;

            string sFileJson = Path.Combine(contentRootPath, licenseFileFolder, licenseFileName);
            if (sFileJson == null || sFileJson.Length == 0 || !System.IO.File.Exists(sFileJson))
            {
                NotFound();
            }

           string result = JSONUtility.UpdateJSONData(licData, sFileJson);
            
           return Json(result);
        }
    }
}
