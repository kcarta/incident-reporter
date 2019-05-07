using System;

namespace IncidentReporter.Shared
{
    public class ActionTaken
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Description { get; set; }
    }
}