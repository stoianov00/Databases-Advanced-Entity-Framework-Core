﻿using System.Collections.Generic;

namespace _02.FootballBetting.Data.Models
{
    public class Position
    {
        public Position()
        {
            this.Players = new HashSet<Player>();     
        }

        public int PositionId { get; set; }

        public string Name { get; set; }

        public ICollection<Player> Players { get; set; } 
    }
}