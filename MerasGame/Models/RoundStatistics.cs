using System.Collections.Generic;

namespace MerasGame.Models
{
    public class RoundStatistics
    {
        public int BudgetChange { get; set; }
        public int PopulationChange { get; set; }
        public int HappinessChange { get; set; }
        public int SecurityChange { get; set; }
        public int EnvironmentChange { get; set; }
        public int InfrastructureChange { get; set; }
        public int BuildingsChange { get; set; }

        public bool HasChanges()
        {
            return BudgetChange != 0 || PopulationChange != 0 || HappinessChange != 0 ||
                   SecurityChange != 0 || EnvironmentChange != 0 || InfrastructureChange != 0 ||
                   BuildingsChange != 0;
        }

        public string GetSummary(string cityName)
        {
            if (!HasChanges())
            {
                return $"{cityName}: No changes";
            }

            var parts = new List<string>();
            
            if (BudgetChange != 0)
                parts.Add($"Budget: {BudgetChange:+#;-#;0}");
            if (PopulationChange != 0)
                parts.Add($"Population: {PopulationChange:+#;-#;0}");
            if (HappinessChange != 0)
                parts.Add($"Happiness: {HappinessChange:+#;-#;0}");
            if (SecurityChange != 0)
                parts.Add($"Security: {SecurityChange:+#;-#;0}");
            if (EnvironmentChange != 0)
                parts.Add($"Environment: {EnvironmentChange:+#;-#;0}");
            if (InfrastructureChange != 0)
                parts.Add($"Infrastructure: {InfrastructureChange:+#;-#;0}");
            if (BuildingsChange != 0)
                parts.Add($"Buildings: {BuildingsChange:+#;-#;0}");

            return $"{cityName}: {string.Join(", ", parts)}";
        }
    }
}
