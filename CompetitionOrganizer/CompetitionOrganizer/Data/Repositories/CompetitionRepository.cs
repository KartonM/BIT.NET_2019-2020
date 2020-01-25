using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompetitionOrganizer.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CompetitionOrganizer.Data.Repositories
{
    class CompetitionRepository
    {
        private CompetitionOrganizerDbContext _db;

        public CompetitionRepository()
        {
            _db = new CompetitionOrganizerDbContext();
        }

        public IEnumerable<Competition> Competitions => _db.Competitions.Include(c => c.Matches);

        public Competition GetCompetitionById(int competitionId) =>
            Competitions.FirstOrDefault(c => c.CompetitionId == competitionId);

        public void AddCompetition(Competition competition)
        {
            _db.Competitions.Add(competition);
            _db.SaveChanges();
        }
    }
}
