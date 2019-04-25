using IncidentReporter.Shared;
using LiteDB;
using System.Collections.Generic;
using System.Linq;

namespace IncidentReporter.Server
{
    public class IncidentService
    {
        private LiteCollection<Incident> incidents;
        public IncidentService(LiteDbContext dbContext)
        {
            incidents = dbContext.Context.GetCollection<Incident>();
        }

        public Incident GetIncidentById(string Id)
        {
            return incidents.FindOne(incident => incident.Id == Id);
        }

        public List<Incident> GetIncidents()
        {
            return incidents.FindAll().ToList();
        }

        public void SaveIncident(Incident incident)
        {
            incidents.Upsert(incident.Id, incident);
        }
    }
}