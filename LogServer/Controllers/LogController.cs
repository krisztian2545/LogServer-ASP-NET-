using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LogServer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LogServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {

        [HttpGet]
        public ActionResult<string> Get()
        {
            Console.WriteLine("GET app names...");
            IEnumerable<string> appNames = LogManager.GetAppNames();
            Console.WriteLine("- " + string.Join(",", appNames.ToArray()) );

            if (appNames != null)
                return Ok(appNames);

            return NotFound();
        }

        [HttpGet("{AppName}")]
        public ActionResult<LogMessage> Get(string AppName)
        {
            Console.WriteLine("GET App logs");
            IEnumerable<LogMessage> logs = LogManager.GetAppLogMessages(AppName.Trim());
            
            if(logs != null)
                return Ok(logs);

            return NotFound();
        }

        [HttpPost]
        public ActionResult Post(LogMessage log)
        {

            Console.WriteLine("Logging: " + log.Message);

            LogManager.SaveLogMessage(log);

            return Ok();
        }

    }
}
