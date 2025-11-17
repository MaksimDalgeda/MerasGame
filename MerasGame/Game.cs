using MerasGame.Models;
using MerasGame.Models.Factories;
using MerasGame.Managers;
using System;

namespace MerasGame
{

    public class Game
    {
        private readonly GameState _gameState;
        private readonly GameUI _ui;
        private readonly EventProcessor _eventProcessor;
        private readonly SpecialEventProcessor _specialEventProcessor;
        private readonly PlayerActionHandler _playerActionHandler;
        private readonly CityUpdater _cityUpdater;

        public Game(City city, int targetRounds)
        {

            _gameState = new GameState(city, targetRounds);
            _ui = new GameUI();

            var rng = new Random();
            var events = EffectFactory.CreateRandomEvents();
            var specialEvents = EffectFactory.CreateSpecialEvents();
            var conditions = EffectFactory.CreateCityConditions();

            _eventProcessor = new EventProcessor(events, rng);
            _specialEventProcessor = new SpecialEventProcessor(specialEvents);
            _playerActionHandler = new PlayerActionHandler();
            _cityUpdater = new CityUpdater(conditions);
        }

        public void Run()
        {
            _ui.ShowWelcomeScreen(_gameState);

            bool exitGame = false;
            while (!exitGame)
            {
                _gameState.CurrentRound++;
                StartNewRound();

                ProcessRound();
                
                if (CheckGameEnd(out exitGame)) break;
                if (_ui.AskToContinue())
                {
                    exitGame = true;
                    Console.WriteLine("Exiting the game");
                }
            }

            _ui.ShowFinalResults(_gameState);
        }

        private void StartNewRound()
        {
            _ui.ShowRoundStart(_gameState);

            _gameState.RoundStartSnapshot = _gameState.City.CreateSnapshot();
        }

        private void ProcessRound()
        {
            _specialEventProcessor.CheckAndApplySpecialEvents(_gameState.City);

            _eventProcessor.ProcessRandomEvents(_gameState.City);

            _playerActionHandler.ProcessPlayerActions(_gameState);

            _cityUpdater.UpdateCity(_gameState.City);
        }

        private bool CheckGameEnd(out bool exit)
        {
            exit = false;

            if (_gameState.IsGameOver())
            {
                _ui.ShowGameOver();
                exit = true;
                return true;
            }

            if (_gameState.IsVictory())
            {
                _ui.ShowVictory();
                exit = true;
                return true;
            }

            return false;
        }
    }
}