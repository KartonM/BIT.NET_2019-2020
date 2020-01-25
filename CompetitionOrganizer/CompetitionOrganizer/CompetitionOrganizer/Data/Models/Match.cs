using System;
using System.Collections.Generic;
using System.Text;

namespace CompetitionOrganizer.Data.Models
{
    class Match
    {
        public int MatchId { get; set; }
        public DateTime Date { get; set; }

        public int CompetitionId { get; set; }
        public Competition Competition { get; set; }
    }
}
