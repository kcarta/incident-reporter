using LiteDB;
using Microsoft.Extensions.Options;
using System;

namespace IncidentReporter.Server
{
    public class LiteDbContext
    {
        public LiteDatabase Context { get; }

        public LiteDbContext(IOptions<DbConfig> configuration)
        {
            try
            {
                var db = new LiteDatabase(configuration.Value.Path);
                if(db != null)
                {
                    Context = db;
                }
            } 
            catch (Exception e)
            {
                throw new Exception($"Cannot create or load database at: {configuration.Value.Path}", e);
            }
        }
    }
}
