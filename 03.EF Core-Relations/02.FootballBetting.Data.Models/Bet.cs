using System;
using _02.FootballBetting.Data.Models.Enums;

namespace _02.FootballBetting.Data.Models
{
    public class Bet
    {
        public int BetId { get; set; }

        public decimal Amount { get; set; }

        public GameResult Prediction { get; set; }

        public DateTime DateTime { get; set; }

        public int UserId { get; set; }

        public User User { get; set; } 

        public int GameId { get; set; }

        public Game Game { get; set; }
    }
}