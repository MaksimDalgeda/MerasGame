using MerasGame.Models;
using MerasGame.Models.Enums;

namespace Game.Tests
{
    [TestFixture]
    public class CityTests
    {

        [Test]
        public void City_IsLost_WhenBudgetIsZero()
        {

            var city = new City("Test City", Difficulty.Normal);

            city.Spend(city.Budget);

            Assert.That(city.IsLost, Is.True);
            Assert.That(city.Budget, Is.EqualTo(0));
        }

        [Test]
        public void City_IsLost_WhenHappinessIsZero()
        {
            var city = new City("Test City", Difficulty.Normal);

            city.ChangeHappiness(-city.Happiness);


            Assert.That(city.IsLost, Is.True);
            Assert.That(city.Happiness, Is.EqualTo(0));
        }

    }

    [TestFixture]
    public class GameStateTests
    {
        [Test]
        public void GameState_IsVictory_WhenRoundsCompleted()
        {
            var city = new City("Test City", Difficulty.Normal);
            var gameState = new GameState(city, targetRounds: 10);

            gameState.CurrentRound = 10;

            Assert.That(gameState.IsVictory(), Is.True);
            Assert.That(city.IsLost, Is.False);
        }

    }


    [TestFixture]
    public class CitySnapshotTests
    {

        [Test]
        public void City_CalculateChanges_ShowsCorrectDifferences()
        {
            var city = new City("Test City", Difficulty.Normal);
            var snapshot = city.CreateSnapshot();

            city.Gain(100);
            city.ChangeHappiness(5);
            city.ChangePopulation(50);

            var stats = city.CalculateChanges(snapshot);

            Assert.That(stats.BudgetChange, Is.EqualTo(100));
            Assert.That(stats.HappinessChange, Is.EqualTo(5));
            Assert.That(stats.PopulationChange, Is.EqualTo(50));
        }
    }

    [TestFixture]
    public class CityStatTests
    {

        [Test]
        public void City_ChangeEnvironment_ClampsToValidRange()
        {
            var city = new City("Test City", Difficulty.Normal);

            city.ChangeEnvironment(1000); // Daugiau minimumo

            Assert.That(city.Environment, Is.LessThanOrEqualTo(100));
        }
    }
}