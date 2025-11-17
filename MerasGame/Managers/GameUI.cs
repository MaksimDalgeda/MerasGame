using MerasGame.Models;
using System;

namespace MerasGame.Managers
{
    public class GameUI
    {
        public void ShowWelcomeScreen(GameState gameState)
        {
            Console.Clear();
            Console.WriteLine("=== MAYOR CITY MANAGEMENT ===");
            Console.WriteLine($"You manage {gameState.City.Name}. Survive {gameState.TargetRounds} rounds to win\n");
            InputHelper.PressAnyKeyToContinue();
        }

        public void ShowRoundStart(GameState gameState)
        {
            Console.Clear();
            Console.WriteLine($"--- Round {gameState.CurrentRound} of {gameState.TargetRounds} ---");
            Console.WriteLine();
            
            if (gameState.RoundStartSnapshot != null)
            {
                ShowRoundSummary(gameState);
                Console.WriteLine();
            }
        }

        public void ShowRoundSummary(GameState gameState)
        {
            var city = gameState.City;
            if (gameState.RoundStartSnapshot == null)
            {
                return;
            }

            var stats = city.CalculateChanges(gameState.RoundStartSnapshot);
            
            Console.WriteLine("=== Current State ===");
            Console.ForegroundColor = city.IsLost ? ConsoleColor.Red : ConsoleColor.White;
            Console.Write($"{city.Name}: ");
            Console.ResetColor();

            Console.Write($"Population={city.Population}");
            ShowChange(stats.PopulationChange);
            Console.Write(", ");

            Console.Write($"Budget={city.Budget}");
            ShowChange(stats.BudgetChange);
            Console.Write(", ");
    
            Console.Write($"Happiness={city.Happiness}");
            ShowChange(stats.HappinessChange);
            Console.Write(", ");
            
            Console.Write($"Security={city.Security}");
            ShowChange(stats.SecurityChange);
            Console.Write(", ");
            
            Console.Write($"Enviroment={city.Environment}");
            ShowChange(stats.EnvironmentChange);
            Console.Write(", ");
            
            Console.Write($"Infrastructure?ra={city.Infrastructure}");
            ShowChange(stats.InfrastructureChange);
            Console.Write(", ");
            
            Console.Write($"Buildings={city.Buildings.Count}");
            ShowChange(stats.BuildingsChange);
            
            if (city.IsLost)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("  [GAME OVER");
                Console.ResetColor();
            }
            
            Console.WriteLine();
        }

        private void ShowChange(int change)
        {
            if (change == 0)
            {
                return;
            }
            
            Console.Write(" (");
            if (change > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"+{change}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{change}");
            }
            Console.ResetColor();
            Console.Write(")");
        }

        public void ShowGameOver()
        {
            Console.WriteLine("\nYour city was lost. YOU LOST.");
        }

        public void ShowVictory()
        {
            Console.WriteLine("\nYou have survived the required number of rounds");
            Console.WriteLine("\nVictory! Your city survived.");
        }

        public void ShowFinalResults(GameState gameState)
        {
            Console.WriteLine("\n=== Final city status ===");
            var city = gameState.City;
            Console.WriteLine(city.StatusLine() + (city.IsLost ? "  [LOST]" : "  [SURVIVED]"));

            Console.WriteLine("\nThank you for playing - Mayor City Game");
            InputHelper.PressAnyKeyToContinue();
        }

        public bool AskToContinue()
        {
            Console.WriteLine("\nPress Enter to continue to the next round, or type Q to exit.");
            var key = Console.ReadLine();
            return !string.IsNullOrWhiteSpace(key) && key.Trim().Equals("q", StringComparison.OrdinalIgnoreCase);
        }
    }
}
