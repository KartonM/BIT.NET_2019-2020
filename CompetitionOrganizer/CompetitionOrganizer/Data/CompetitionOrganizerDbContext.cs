using System;
using System.Collections.Generic;
using System.Text;
using CompetitionOrganizer.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CompetitionOrganizer.Data
{
    class CompetitionOrganizerDbContext : DbContext
    {
        public DbSet<Match> Matches { get; set; }
        public DbSet<Competition> Competitions { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\MSSQLLocalDB;Database=CompetitionOrganizer;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
