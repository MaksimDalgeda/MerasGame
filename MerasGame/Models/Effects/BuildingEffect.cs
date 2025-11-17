using System;
using System.Collections.Generic;

namespace MerasGame.Models.Effects
{
    public class BuildingEffect : GameEffect
    {
        public int HappinessChange { get; }
        public int IncomeChange { get; }
        public int PopulationChange { get; }
        public int EnvironmentChange { get; }
        public int MaintenanceCost { get; }

        private readonly Action<City>? _customEffect;

        public BuildingEffect(
            string name,
            string description,
            int happinessChange = 0,
            int incomeChange = 0,
            int populationChange = 0,
            int environmentChange = 0,
            int maintenanceCost = 0,
            Action<City>? customEffect = null)
            : base(name, description)
        {
            HappinessChange = happinessChange;
            IncomeChange = incomeChange;
            PopulationChange = populationChange;
            EnvironmentChange = environmentChange;
            MaintenanceCost = Math.Max(0, maintenanceCost);
            _customEffect = customEffect;
        }

        protected override void ApplyEffect(City city)
        {
            if (HappinessChange != 0)
            {
                city.ChangeHappiness(HappinessChange);
            }

            if (PopulationChange != 0)
            {
                city.ChangePopulation(PopulationChange);
            }

            if (EnvironmentChange != 0)
            {
                city.ChangeEnvironment(EnvironmentChange);
            }

            if (IncomeChange > 0)
            {
                city.Gain(IncomeChange);
            }
            else if (IncomeChange < 0)
            {
                city.Spend(Math.Abs(IncomeChange));
            }

            if (MaintenanceCost > 0)
            {
                city.Spend(MaintenanceCost);
            }

            _customEffect?.Invoke(city);
        }

        public string GetEffectSummary()
        {
            var parts = new List<string>();

            if (IncomeChange != 0)
                parts.Add($"Income: {IncomeChange:+#;-#;0}");
            if (HappinessChange != 0)
                parts.Add($"Happiness: {HappinessChange:+#;-#;0}");
            if (PopulationChange != 0)
                parts.Add($"Population: {PopulationChange:+#;-#;0}");
            if (EnvironmentChange != 0)
                parts.Add($"Environment: {EnvironmentChange:+#;-#;0}");
            if (MaintenanceCost > 0)
                parts.Add($"Maintenance: -{MaintenanceCost}");

            return parts.Count > 0 ? string.Join(", ", parts) : "No effects";
        }
    }
}
