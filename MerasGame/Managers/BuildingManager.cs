using MerasGame.Models;
using MerasGame.Models.Enums;
using MerasGame.Models.Factories;
using System;

namespace MerasGame.Managers
{
    public class BuildingManager
    {
        public void ManageCityBuildings(City city)
        {
            while (true)
            {
                ShowBuildingsMenu(city);
                
                if (city.Buildings.Count > 0)
                {
                    var choice = InputHelper.ReadChoice(1, 3);
                    if (choice == 1) HandleBuildBuilding(city);
                    else if (choice == 2) HandleDemolishBuilding(city);
                    else break;
                }
                else
                {
                    var choice = InputHelper.ReadChoice(1, 2);
                    if (choice == 1) HandleBuildBuilding(city);
                    else break;
                }
            }
        }

        private void ShowBuildingsMenu(City city)
        {
            Console.WriteLine($"\n--- Building Management: {city.Name} ---");
            Console.WriteLine($"Budget: {city.Budget}");
            
            if (city.Buildings.Count > 0)
            {
                Console.WriteLine("\nExisting buildings:");
                for (int i = 0; i < city.Buildings.Count; i++)
                {
                    Console.WriteLine($"  {i + 1}. {city.Buildings[i]}");
                }
            }
            else
            {
                Console.WriteLine("\nThe city has no buildings yet.");
            }

            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("1) Build a new building");
            if (city.Buildings.Count > 0)
            {
                Console.WriteLine("2) Demolish a building");
                Console.WriteLine("3) Finish building management");
            }
            else
            {
                Console.WriteLine("2) Finish building management");
            }
        }

        private void HandleBuildBuilding(City city)
        {
            Console.WriteLine("\n--- New Building Construction ---");
            Console.WriteLine("Choose building type:");
            
            var buildingTypes = BuildingFactory.GetAvailableBuildingTypes();
            for (int i = 0; i < buildingTypes.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {BuildingFactory.GetBuildingDescription(buildingTypes[i])}");
            }
            Console.WriteLine($"{buildingTypes.Count + 1}) Cancel");
            
            int buildChoice = InputHelper.ReadChoice(1, buildingTypes.Count + 1);
            if (buildChoice == buildingTypes.Count + 1) return;

            BuildingType buildingType = buildingTypes[buildChoice - 1];
            
            if (city.TryBuildBuilding(buildingType))
            {
                var lastBuilding = city.Buildings[^1];
                Console.WriteLine($"\n Successfully built: {lastBuilding}");
            }
            else
            {
                Console.WriteLine("\n Insufficient budget for construction!");
            }
        }

        private void HandleDemolishBuilding(City city)
        {
            Console.WriteLine("\n--- Building Demolition ---");
            for (int i = 0; i < city.Buildings.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {city.Buildings[i]}");
            }
            Console.WriteLine($"{city.Buildings.Count + 1}) Cancel");

            int demolishChoice = InputHelper.ReadChoice(1, city.Buildings.Count + 1);
            if (demolishChoice == city.Buildings.Count + 1) return;

            var buildingToRemove = city.Buildings[demolishChoice - 1];
            if (city.TryDemolishBuilding(buildingToRemove.Id))
            {
                Console.WriteLine($"\n Demolished: {buildingToRemove.Name}");
            }
        }
    }
}
