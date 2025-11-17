using System;
using MerasGame.Models;
using MerasGame.Managers;
using MerasGame.Models.Enums;

namespace MerasGame
{
    class Program
    {
        static void Main()
        {

            Console.Title = "Mayor City Management";

            Console.WriteLine("Welcome — Manage your city and survive the challenges");
            
            Console.Write("Enter your city name or leave empty for default: ");
            var name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name)) name = "My City";


            Console.WriteLine("\nSelect difficulty level:");
            Console.WriteLine("1. Easy - More resources, easier start");
            Console.WriteLine("2. Normal - Balanced gameplay");
            Console.WriteLine("3. Hard - Limited resources, challenging");
            
            int difficultyChoice = InputHelper.PromptInt("Choose difficulty (1-3)", 1, 3);
            
            Difficulty difficulty = difficultyChoice switch
            {
                1 => Difficulty.Easy,
                2 => Difficulty.Normal,
                3 => Difficulty.Hard,
                _ => Difficulty.Normal
            };

            var city = new City(name, difficulty);

            int rounds = InputHelper.PromptInt("How many rounds do you need to survive to win? (5-100)", 5, 100);

            var game = new Game(city, rounds);
            game.Run();
        }
    }
}