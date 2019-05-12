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
                .AppendLine("# Incident report:")
                .AppendLine("\n## Date")
                .AppendLine(incident.Date.ToString("dd-MM-yyyy"))
                .AppendLine("\n## Summary")
                .AppendLine(incident.Summary)
                .AppendLine("\n## Status")
                .AppendLine(incident.IsResolved ? "Resolved" : "Ongoing")
                .AppendLine("\n## Impact")
                .AppendLine(incident.Impact)
                .AppendLine("\n## Root Cause")
                .AppendLine(incident.RootCause)
                .AppendLine("\n## Trigger")
                .AppendLine(incident.Trigger)
                .AppendLine("\n## Timeline")
                .AppendLine("\n| Time | Description |")
                .AppendLine("| ---- | ----------- |");
            foreach (var action in incident.ActionsTaken)
            {
                var timestamp = action.Timestamp.ToString("dd-MM-yy HH:mm");
                text.AppendLine($"| {timestamp} | {action.Description} |");
            }
            text.AppendLine("\n## Resolution")
                .AppendLine(incident.Resolution)
                .AppendLine("\n## Detection")
                .AppendLine(incident.Detection)
                .AppendLine("\n## Lessons Learned")
                .AppendLine(incident.LessonsLearned);
            return new JsonResult(text.ToString());
        }
    }
}
