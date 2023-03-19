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
        public const string licenseFileName = "license-config.json";
        public const string licenseFileFolder = "LicenseFile";
        public static IWebHostEnvironment? _webHostEnvironment;        

        public LicenseHandlerController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }       

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

           int result = JSONUtility.UpdateJSONData(licData, sFileJson);
            
           return Json(result);
        }
    }
}
