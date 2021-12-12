using DBContextSQLite.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBContextSQLite
{
    public class VideoMonitoringContext : DbContext
    {
        public DbSet<Server> Server { get; set; }
        public DbSet<Video> Video { get; set; }
        private readonly IConfiguration _configuration;

        public  VideoMonitoringContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }   
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var sourceSQLite = _configuration.GetSection("Repository").GetSection("SourceSQLite").Value;
            optionsBuilder.UseSqlite($"Data Source={sourceSQLite}");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
