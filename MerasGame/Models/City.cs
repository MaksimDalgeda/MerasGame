using System;
using System.Collections.Generic;
using System.Linq;
using MerasGame.Models.Enums;
using MerasGame.Models.Factories;

namespace MerasGame.Models
{
    public class City
    {
        public string Name { get; }
        public int Population { get; private set; }
        public int Budget { get; private set; }
        public int Happiness { get; private set; }  
        public int Security { get; private set; } 
        public int Environment { get; private set; }
        public int Infrastructure { get; private set; }
        public bool IsLost => Budget <= 0 || Happiness <= 0 || Population <= 0;
        public List<Building> Buildings { get; } = new();
        public List<CityCondition> ActiveConditions { get; } = new();

        private static readonly Random Rng = new();

        public City(string name, Difficulty difficulty = Difficulty.Normal)
        {
            Name = name;
            
            switch (difficulty)
            {
                case Difficulty.Easy:
                    Population = Rng.Next(1500, 2500);
                    Budget = Rng.Next(1500, 2500);
                    Happiness = Rng.Next(75, 90);
                    Security = Rng.Next(75, 90);
                    Environment = Rng.Next(75, 90);
                    Infrastructure = Rng.Next(2, 5);
                    break;
                    
                case Difficulty.Hard:
                    Population = Rng.Next(500, 1000);
                    Budget = Rng.Next(500, 1000);
                    Happiness = Rng.Next(50, 65);
                    Security = Rng.Next(50, 65);
                    Environment = Rng.Next(50, 65);
                    Infrastructure = Rng.Next(0, 2);
                    break;
                    
                case Difficulty.Normal:
                default:
                    Population = Rng.Next(1000, 1500);
                    Budget = Rng.Next(1000, 1500);
                    Happiness = Rng.Next(65, 80);
                    Security = Rng.Next(65, 80);
                    Environment = Rng.Next(65, 80);
                    Infrastructure = Rng.Next(1, 3);
                    break;
            }
        }

        public void UpdateConditions(List<CityCondition> allConditions)
        {
            ActiveConditions.Clear();
            
            foreach (var condition in allConditions)
            {
                condition.UpdateStatus(this);
                if (condition.IsActive)
                {
                    ActiveConditions.Add(condition);
                }
            }
        }

        public void ApplyActiveConditions()
        {
            foreach (var condition in ActiveConditions)
            {
                condition.Execute(this);
            }
        }

        public CitySnapshot CreateSnapshot()
        {
            return new CitySnapshot
            {
                Population = Population,
                Budget = Budget,
                Happiness = Happiness,
                Security = Security,
                Environment = Environment,
                Infrastructure = Infrastructure,
                BuildingsCount = Buildings.Count
            };
        }

        public RoundStatistics CalculateChanges(CitySnapshot previous)
        {
            return new RoundStatistics
            {
                BudgetChange = Budget - previous.Budget,
                PopulationChange = Population - previous.Population,
                HappinessChange = Happiness - previous.Happiness,
                SecurityChange = Security - previous.Security,
                EnvironmentChange = Environment - previous.Environment,
                InfrastructureChange = Infrastructure - previous.Infrastructure,
                BuildingsChange = Buildings.Count - previous.BuildingsCount
            };
        }

        public bool TryBuildBuilding(BuildingType type)
        {
            var building = BuildingFactory.CreateBuilding(type);

            if (Budget < building.BuildCost)
            {
                return false;
            }

            Spend(building.BuildCost);
            Buildings.Add(building);
            return true;
        }

        public bool TryDemolishBuilding(int buildingId)
        {
            var building = Buildings.FirstOrDefault(b => b.Id == buildingId);
            if (building == null)
            {
                return false; 
            }

            Buildings.Remove(building);
            Gain(building.BuildCost / 2);
            return true;
        }

        public void ApplyAction(ActionType action)
        {
            if (IsLost) return;

            switch (action)
            {
                case ActionType.Build:
                    Spend(200 + Infrastructure * 50);
                    Infrastructure = Math.Min(10, Infrastructure + 1);
                    Happiness = Clamp(Happiness + 8 + Infrastructure / 2);
                    Environment = Clamp(Environment - 3);
                    break;

                case ActionType.Repair:
                    Spend(120 + Infrastructure * 20);
                    Security = Clamp(Security + 12);
                    Happiness = Clamp(Happiness + 4);
                    break;

                case ActionType.IncreaseTaxes:
                    var taxIncome = 150 + (Population / 10);
                    Gain(taxIncome);
                    Happiness = Clamp(Happiness - 10);
                    break;

                case ActionType.ReduceExpenses:
                    var saved = 100 + (Infrastructure * 10);
                    Gain(saved);
                    Security = Clamp(Security - 8);
                    Environment = Clamp(Environment - 5);
                    Happiness = Clamp(Happiness - 6);
                    break;

                case ActionType.Skip:
                default:
                    break;
            }
        }

        public void ApplyRandomEvent(CityEvent e)
        {
            if (IsLost) return;
            e.Execute(this);
        }

        public void EndOfRoundUpdate()
        {
            if (IsLost) return;

            ApplyBuildingEffects();
            ApplyNaturalChanges();
            ApplyMaintenance();
            ApplyPopulationGrowth();
            ApplyEnvironmentDecay();

            if (Budget < 0) Budget = 0;
            if (IsLost) return;
        }

        private void ApplyBuildingEffects()
        {
            foreach (var building in Buildings)
            {
                building.ApplyEffectToCity(this);
            }
        }

        private void ApplyNaturalChanges()
        {
            Happiness = Clamp(Happiness + Infrastructure / 2);
            Security = Clamp(Security + Infrastructure / 3);
        }

        private void ApplyMaintenance()
        {
            var maintenance = 50 + Infrastructure * 10 + (Population / 50);
            Spend(maintenance);
        }

        private void ApplyPopulationGrowth()
        {
            if (Happiness > 75 && Environment > 70 && Rng.NextDouble() < 0.25)
            {
                var grow = Math.Max(1, Population / 20);
                Population += grow;
                Happiness = Clamp(Happiness + 1);
            }
        }

        private void ApplyEnvironmentDecay()
        {
            Environment = Clamp(Environment - 1);
        }

        public void Spend(int amount)
        {
            Budget -= Math.Max(0, amount);
            if (Budget < 0) Budget = 0;
        }

        public void Gain(int amount)
        {
            Budget += Math.Max(0, amount);
        }

        public void ChangeHappiness(int delta) => Happiness = Clamp(Happiness + delta);
        public void ChangeSecurity(int delta) => Security = Clamp(Security + delta);
        public void ChangeEnvironment(int delta) => Environment = Clamp(Environment + delta);
        public void ChangePopulation(int delta) => Population = Math.Max(0, Population + delta);

        public string StatusLine()
        {
            string conditionsInfo = ActiveConditions.Any() 
                ? $" [! {ActiveConditions.Count} active conditions]" 
                : "";
            
            return $"{Name}: Population={Population}, Budget={Budget}, Happiness={Happiness}, " +
                   $"Security={Security}, Environment={Environment}, Infrastructure={Infrastructure}, " +
                   $"Buildings={Buildings.Count}{conditionsInfo}";
        }

        private int Clamp(int value) => Math.Max(0, Math.Min(100, value));
    }
}