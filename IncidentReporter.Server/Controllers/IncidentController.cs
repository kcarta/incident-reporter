using IncidentReporter.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncidentReporter.Server.Controllers
{
    [Route("api/[controller]")]
    public class IncidentController : Controller
    {
        private IncidentService incidentService;
        private ILogger logger;

        public IncidentController(ILogger<IncidentController> logger, IncidentService incidentService)
        {
            this.logger = logger;
            this.incidentService = incidentService;
        }


        [HttpGet("[action]")]
        public IEnumerable<Incident> Incidents()
        {
            logger.LogInformation("Retrieving all incidents");
            return incidentService.GetIncidents();
        }
    }
}
