﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime Time { get; set; }
        public int TournamentId { get; set; }

    }
}
