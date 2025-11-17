using System;
using MerasGame.Models.Enums;
using MerasGame.Models.Effects;

namespace MerasGame.Models
{
    public class Building
    {
        private static int _nextId = 1;
        
        public int Id { get; }
        public BuildingType Type { get; }
        public string Name { get; }
        public int BuildCost { get; }
        public BuildingEffect Effect { get; }

        public Building(BuildingType type, string name, int buildCost, BuildingEffect effect)
        {
            Id = _nextId++;
            Type = type;
            Name = name;
            BuildCost = buildCost;
            Effect = effect ?? throw new ArgumentNullException(nameof(effect));
        }

        public void ApplyEffectToCity(City city)
        {
            Effect.Execute(city);
        }

        public override string ToString()
        {
            return $"{Name} (ID: {Id}) - {Effect.GetEffectSummary()}";
        }
    }
}
