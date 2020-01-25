using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CompetitionOrganizer.Data.Models
{
    class Competition
    {
        public int CompetitionId { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public DateTime StarDateTime { get; set; }

        public List<Match> Matches { get; set; }
    }
}
