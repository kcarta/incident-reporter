using IncidentReporter.Shared;
using LiteDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IncidentReporter.Server.Controllers
{
    [Route("api/[controller]")]
    public class IncidentController : Controller
    {
        private readonly ILogger logger;
        private LiteCollection<Incident> incidents;

        public IncidentController(ILogger<IncidentController> logger, LiteDbContext db)
        {
            this.logger = logger;
            incidents = db.Context.GetCollection<Incident>();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Incident>> GetIncidents()
        {
            logger.LogInformation("Retrieving all incidents");
            var result = incidents.FindAll().ToList();
            return result;
        }

        [HttpGet("{id}")]
        public ActionResult<Incident> Get(string id)
        {
            logger.LogInformation($"Retrieving incident with Id: {id}");
            var result = incidents.FindOne(incident => incident.Id == id);
            return result;
        }

        [HttpPost]
        public void Post([FromBody] Incident incident)
        {
            logger.LogInformation($"Saving incident with Id: {incident.Id}");
            incidents.Upsert(incident.Id, incident);
        }

        [Route("Export")]
        [HttpPost]
        public JsonResult Export([FromBody] Incident incident)
        {
            var text = new StringBuilder()
                .AppendLine(incident.Summary)
                .ToString();
            return new JsonResult(text);
        }
    }
}
