using MerasGame.Models;
using MerasGame.Models.Enums;
using System;
using System.Linq;

namespace MerasGame.Managers
{
    public class PlayerActionHandler
    {
        public void ProcessPlayerActions(GameState gameState)
        {
            var city = gameState.City;
            
            if (city.IsLost)
            {
                Console.WriteLine($"{city.Name} is already lost. Skipping.");
                return;
            }

            Console.WriteLine(city.StatusLine());
            Console.WriteLine();
            ProcessCityTurn(city);
        }

        private void ProcessCityTurn(City city)
        {
            Console.WriteLine($"\n=== City: {city.Name} ===");
            
            ShowActiveConditions(city);
            
            ShowActionMenu();
            var action = GetPlayerAction();
            city.ApplyAction(action);
            Console.WriteLine($"Action applied.");
            ClearLines(8);
            var buildingManager = new BuildingManager();
            buildingManager.ManageCityBuildings(city);
        }

        private void ShowActiveConditions(City city)
        {
            if (city.ActiveConditions.Any())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  ACTIVE CONDITIONS:");
                foreach (var condition in city.ActiveConditions)
                {
                    Console.WriteLine($"   ? {condition.Name}: {condition.Description}");
                }
                Console.ResetColor();
                Console.WriteLine();
            }
        }

        private void ShowActionMenu()
        {
            Console.WriteLine("Choose an action:");
            Console.WriteLine("1) Build infrastructure (expensive, increases infrastructure and happiness)");
            Console.WriteLine("2) Repair (medium cost, increases security)");
            Console.WriteLine("3) Increase taxes (gain budget, decreases happiness)");
            Console.WriteLine("4) Reduce expenses (save budget, decreases services)");
            Console.WriteLine("5) Skip");
        }

        private ActionType GetPlayerAction()
        {
            var actionChoice = InputHelper.ReadChoice(1, 5);
            return actionChoice switch
            {
                1 => ActionType.Build,
                2 => ActionType.Repair,
                3 => ActionType.IncreaseTaxes,
                4 => ActionType.ReduceExpenses,
                _ => ActionType.Skip
            };
        }

        private static void ClearLines(int Lines)
        {
            for (int i = 0; i < Lines; i++)
            {
                int currentLine = Console.CursorTop - 1;
                Console.SetCursorPosition(0, currentLine);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, currentLine);
            }
        }
    }
}
