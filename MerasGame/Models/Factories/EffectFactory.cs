using System;
using System.Collections.Generic;
using MerasGame.Models.Enums;
using MerasGame.Models.Effects;

namespace MerasGame.Models.Factories
{
    public class EffectFactory
    {
        private const int LOW_SECURITY_THRESHOLD = 20;
        private const int LOW_HAPPINESS_THRESHOLD = 30;
        private const int LOW_ENVIRONMENT_THRESHOLD = 25;
        private const int HIGH_POPULATION_THRESHOLD = 2000;
        private const int LOW_BUDGET_THRESHOLD = 200;

        private const int POPULATION_LOSS_RATE = 50;
        private const int UNREST_HAPPINESS_PENALTY = 5;
        private const int POLLUTION_HEALTH_PENALTY = 3;
        private const int OVERPOPULATION_PENALTY = 100;
        private const int POVERTY_SECURITY_PENALTY = 4;

        public static List<CityEvent> CreateRandomEvents()
        {
            return new List<CityEvent>
            {
                new CityEvent("Fire", "A large fire breaks out in the city.", city =>
                {
                    city.ChangePopulation(-Math.Max(1, city.Population / 50));
                    city.ChangeSecurity(-20);
                    city.Spend(200);
                    city.ChangeHappiness(-12);
                }),
                new CityEvent("Protest", "Residents protest demanding changes.", city =>
                {
                    city.ChangeHappiness(-18);
                    city.Spend(50);
                    if (city.Security < 40)
                    {
                        city.ChangePopulation(-Math.Max(1, city.Population / 200));
                        city.Spend(100);
                    }
                }),
                new CityEvent("Storm", "Severe weather damages environment and infrastructure.", city =>
                {
                    city.ChangeEnvironment(-25);
                    city.ChangeSecurity(-10);
                    city.Spend(150);
                }),
                new CityEvent("Philanthropic Donation", "Generous donors donate to the city.", city =>
                {
                    city.Gain(200);
                    city.ChangeHappiness(10);
                }),
                new CityEvent("Disease Outbreak", "Disease reduces population and happiness.", city =>
                {
                    var lost = Math.Max(1, city.Population / 30);
                    city.ChangePopulation(-lost);
                    city.ChangeHappiness(-15);
                    city.Spend(120);
                }),
                new CityEvent("Economic Boom", "City economy temporarily flourishes.", city =>
                {
                    city.Gain(300 + city.Population / 5);
                    city.ChangeHappiness(8);
                })
            };
        }

        public static List<SpecialEvent> CreateSpecialEvents()
        {
            return new List<SpecialEvent>
            {
                new SpecialEvent(
                    "Mass Protests", 
                    "Residents organize mass protests! Factories cannot operate normally.",
                    city => city.Happiness < 20,
                    city =>
                    {
                        foreach (var building in city.Buildings)
                        {
                            if (building.Type == BuildingType.Factory)
                            {
                                int normalIncome = building.Effect.IncomeChange;
                                int reducedIncome = normalIncome / 2;
                                city.Spend(normalIncome - reducedIncome);
                            }
                        }
                        city.ChangeSecurity(-10);
                        city.ChangeHappiness(-5);
                    }
                ),
                new SpecialEvent(
                    "Economic Crisis", 
                    "City budget is critically low! All buildings cost more to maintain.",
                    city => city.Budget < 100,
                    city =>
                    {
                        foreach (var building in city.Buildings)
                        {
                            city.Spend(building.Effect.MaintenanceCost);
                        }
                        city.ChangeHappiness(-8);
                    }
                ),
                new SpecialEvent(
                    "Environmental Catastrophe", 
                    "Critically polluted environment! Residents are getting sick en masse.",
                    city => city.Environment < 15,
                    city =>
                    {
                        city.ChangePopulation(-Math.Max(5, city.Population / 20));
                        city.ChangeHappiness(-15);
                        city.Spend(200);
                    }
                ),
                new SpecialEvent(
                    "Street Chaos", 
                    "Low security caused chaos! Residents are fleeing the city.",
                    city => city.Security < 10,
                    city =>
                    {
                        city.ChangePopulation(-Math.Max(10, city.Population / 15));
                        city.ChangeHappiness(-20);
                        city.Spend(300);
                    }
                ),
                new SpecialEvent(
                    "Infrastructure Collapse", 
                    "Without infrastructure the city cannot function!",
                    city => city.Infrastructure == 0 && city.Population > 500,
                    city =>
                    {
                        city.ChangePopulation(-Math.Max(20, city.Population / 10));
                        city.ChangeHappiness(-25);
                        city.ChangeSecurity(-15);
                        city.Spend(150);
                    }
                )
            };
        }

        public static List<CityCondition> CreateCityConditions()
        {
            return new List<CityCondition>
            {
                new CityCondition(
                    "Crime",
                    "Low security causes resident emigration",
                    city => city.Security < LOW_SECURITY_THRESHOLD,
                    city => city.ChangePopulation(-Math.Max(1, city.Population / POPULATION_LOSS_RATE))
                ),
                new CityCondition(
                    "Resident Dissatisfaction",
                    "Dissatisfied residents are causing unrest",
                    city => city.Happiness < LOW_HAPPINESS_THRESHOLD,
                    city =>
                    {
                        city.ChangeHappiness(-UNREST_HAPPINESS_PENALTY);
                        city.ChangeSecurity(-3);
                    }
                ),
                new CityCondition(
                    "Environmental Pollution",
                    "Poor environment harms residents' health",
                    city => city.Environment < LOW_ENVIRONMENT_THRESHOLD,
                    city =>
                    {
                        city.ChangePopulation(-Math.Max(1, city.Population / 100));
                        city.ChangeHappiness(-POLLUTION_HEALTH_PENALTY);
                    }
                ),
                new CityCondition(
                    "Overcrowding",
                    "Too many residents overload city infrastructure",
                    city => city.Population > HIGH_POPULATION_THRESHOLD,
                    city =>
                    {
                        city.Spend(OVERPOPULATION_PENALTY);
                        city.ChangeHappiness(-4);
                        city.ChangeEnvironment(-2);
                    }
                ),
                new CityCondition(
                    "Poverty",
                    "Low budget worsens quality of life",
                    city => city.Budget < LOW_BUDGET_THRESHOLD,
                    city =>
                    {
                        city.ChangeHappiness(-6);
                        city.ChangeSecurity(-POVERTY_SECURITY_PENALTY);
                    }
                )
            };
        }

        public static List<BuildingEffect> CreateCustomBuildingEffects()
        {
            return new List<BuildingEffect>
            {
                new BuildingEffect(
                    "Advanced Factory Effect",
                    "Modern factory with environmental technologies",
                    happinessChange: -5,
                    incomeChange: 300,
                    populationChange: 0,
                    environmentChange: -3,
                    maintenanceCost: 80
                ),
                new BuildingEffect(
                    "Park Effect",
                    "Green spaces improve city environment",
                    happinessChange: 8,
                    incomeChange: 0,
                    populationChange: 5,
                    environmentChange: 15,
                    maintenanceCost: 20
                ),
                new BuildingEffect(
                    "School Effect",
                    "Educational institution improves quality of life",
                    happinessChange: 10,
                    incomeChange: -30,
                    populationChange: 10,
                    environmentChange: 0,
                    maintenanceCost: 60
                ),
                new BuildingEffect(
                    "Police Station Effect",
                    "Security enforcement",
                    happinessChange: 0,
                    incomeChange: -50,
                    populationChange: 0,
                    environmentChange: 0,
                    maintenanceCost: 70,
                    customEffect: city => city.ChangeSecurity(15)
                ),
                new BuildingEffect(
                    "Eco Factory Effect",
                    "Factory with dynamic profit based on environment",
                    happinessChange: 2,
                    incomeChange: 0,
                    populationChange: 0,
                    environmentChange: -1,
                    maintenanceCost: 45,
                    customEffect: city =>
                    {
                        int dynamicIncome = 100 + (city.Environment * 2);
                        city.Gain(dynamicIncome);
                    }
                )
            };
        }
    }
}
