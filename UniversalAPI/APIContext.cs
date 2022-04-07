using Microsoft.EntityFrameworkCore;
using System;

namespace UniversalAPI
{
    /// <summary>
    /// Context containing the requests table. Inherit this class, migrate and update the database. <br/>
    /// Method <see cref="OnConfiguring"/> use <see cref="Environment"/> with variable name "UnivarsalAPI.ConnectionString". <br/>
    /// Set variable "UnivarsalAPI.ConnectionString" <see cref="Environment.SetEnvironmentVariable(string, string?)"/> if you use the default constructor. <br/>
    /// Contains an <see cref="APIRequests"/> table with requests. Table model: <see cref="Request"/>.
    /// </summary>
    public class APIContext : DbContext
    {
        public APIContext() : base() { }
        public APIContext(DbContextOptions<APIContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("UniversalAPI.ConnectionString"));
            }
        }

        public virtual DbSet<Request> APIRequests { get; set; }
    }
}
