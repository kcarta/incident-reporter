using System;
using System.Collections.Generic;

namespace IncidentReporter.Shared
{
    public class Incident
    {
        public string Id { get; set; }
        public bool IsResolved { get; set; }
        public DateTime Date { get; set; }
        public string Summary { get; set; }
        public string Impact { get; set; }
        public string RootCause { get; set; }
        public string Trigger { get; set; }
        public string Resolution { get; set; }
        public string Detection { get; set; }
        public string LessonsLearned { get; set; }
        public List<ActionTaken> ActionsTaken { get; set; }
    }
}
