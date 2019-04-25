using IncidentReporter.Shared;
using LiteDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

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
            incidents.Upsert(new Incident { Id = "42", Date = DateTime.Now, Summary = "I am the default incident" });
        }

        [HttpGet]
        public ActionResult<IEnumerable<Incident>> GetIncidents()
        {
            logger.LogInformation("Retrieving all incidents");
            return incidents.FindAll().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Incident> Get(string id)
        {
            logger.LogInformation($"Retrieving incident with Id: {id}");
            return incidents.FindOne(incident => incident.Id == id);
        }

        [HttpPost]
        public void Post([FromBody] Incident incident)
        {
            logger.LogInformation($"Saving incident with Id: {incident.Id}");
            incidents.Upsert(incident.Id, incident);
        }
    }
}
