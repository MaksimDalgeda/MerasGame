using System.Collections.Generic;

namespace MerasGame.Models
{
    public class GameState
    {
        public City City { get; }
        public int CurrentRound { get; set; }
        public int TargetRounds { get; }
        public CitySnapshot? RoundStartSnapshot { get; set; }

        public GameState(City city, int targetRounds)
        {
            City = city;
            TargetRounds = targetRounds;
            CurrentRound = 0;
            RoundStartSnapshot = null;
        }

        public bool IsGameOver()
        {
            return City.IsLost;
        }

        public bool IsVictory()
        {
            return CurrentRound >= TargetRounds && !City.IsLost;
        }
    }
}
