using System;
using System.Collections.Generic;
using System.Linq;
using MerasGame.Models.Effects;
using MerasGame.Models.Enums;

namespace MerasGame.Models.Factories
{
    public class BuildingFactory
    {
        public static Building CreateBuilding(BuildingType type)
        {
            return type switch
            {
                BuildingType.House => CreateHouse(),
                BuildingType.Factory => CreateFactory(),
                BuildingType.Park => CreatePark(),
                BuildingType.School => CreateSchool(),
                BuildingType.Hospital => CreateHospital(),
                BuildingType.PoliceStation => CreatePoliceStation(),
                BuildingType.PowerPlant => CreatePowerPlant(),
                BuildingType.ShoppingMall => CreateShoppingMall(),
                _ => throw new ArgumentException($"Unknown building type: {type}")
            };
        }

        private static Building CreateHouse()
        {
            var effect = new BuildingEffect(
                name: "Residential House Effect",
                description: "Increases population and happiness, but decreases environment",
                happinessChange: 5,
                incomeChange: -50,
                populationChange: 15,
                environmentChange: -5,
                maintenanceCost: 30
            );

            return new Building(
                BuildingType.House,
                "Residential House",
                buildCost: 500,
                effect: effect
            );
        }

        private static Building CreateFactory()
        {
            var effect = new BuildingEffect(
                name: "Factory Effect",
                description: "Generates income, but pollutes environment and decreases happiness",
                happinessChange: -10,
                incomeChange: 200,
                populationChange: 0,
                environmentChange: -8,
                maintenanceCost: 50
            );

            return new Building(
                BuildingType.Factory,
                "Factory",
                buildCost: 800,
                effect: effect
            );
        }

        private static Building CreatePark()
        {
            var effect = new BuildingEffect(
                name: "Park Effect",
                description: "Improves environment and happiness, attracts residents",
                happinessChange: 8,
                incomeChange: 0,
                populationChange: 5,
                environmentChange: 15,
                maintenanceCost: 20
            );

            return new Building(
                BuildingType.Park,
                "Park",
                buildCost: 400,
                effect: effect
            );
        }

        private static Building CreateSchool()
        {
            var effect = new BuildingEffect(
                name: "School Effect",
                description: "Improves education, attracts families",
                happinessChange: 10,
                incomeChange: -30,
                populationChange: 10,
                environmentChange: 0,
                maintenanceCost: 60
            );

            return new Building(
                BuildingType.School,
                "School",
                buildCost: 900,
                effect: effect
            );
        }

        private static Building CreateHospital()
        {
            var effect = new BuildingEffect(
                name: "Hospital Effect",
                description: "Improves residents' health and security",
                happinessChange: 12,
                incomeChange: -80,
                populationChange: 8,
                environmentChange: 0,
                maintenanceCost: 100,
                customEffect: city => city.ChangeSecurity(5)
            );

            return new Building(
                BuildingType.Hospital,
                "Hospital",
                buildCost: 1200,
                effect: effect
            );
        }

        private static Building CreatePoliceStation()
        {
            var effect = new BuildingEffect(
                name: "Police Station Effect",
                description: "Increases city security",
                happinessChange: 3,
                incomeChange: -50,
                populationChange: 0,
                environmentChange: 0,
                maintenanceCost: 70,
                customEffect: city => city.ChangeSecurity(15)
            );

            return new Building(
                BuildingType.PoliceStation,
                "Police Station",
                buildCost: 700,
                effect: effect
            );
        }

        private static Building CreatePowerPlant()
        {
            var effect = new BuildingEffect(
                name: "Power Plant Effect",
                description: "Generates income, but heavily pollutes environment",
                happinessChange: -15,
                incomeChange: 300,
                populationChange: 0,
                environmentChange: -12,
                maintenanceCost: 80,
                customEffect: city =>
                {
                    if (city.Environment < 30)
                    {
                        city.ChangeHappiness(-5);
                    }
                }
            );

            return new Building(
                BuildingType.PowerPlant,
                "Power Plant",
                buildCost: 1500,
                effect: effect
            );
        }

        private static Building CreateShoppingMall()
        {
            var effect = new BuildingEffect(
                name: "Shopping Mall Effect",
                description: "Generates income and increases happiness, attracts residents",
                happinessChange: 15,
                incomeChange: 150,
                populationChange: 20,
                environmentChange: -3,
                maintenanceCost: 90
            );

            return new Building(
                BuildingType.ShoppingMall,
                "Shopping Mall",
                buildCost: 1000,
                effect: effect
            );
        }

        public static List<BuildingType> GetAvailableBuildingTypes()
        {
            return Enum.GetValues<BuildingType>().ToList();
        }

        public static string GetBuildingDescription(BuildingType type)
        {
            var building = CreateBuilding(type);
            return $"{building.Name} - Cost: {building.BuildCost}, {building.Effect.GetEffectSummary()}";
        }
    }
}
