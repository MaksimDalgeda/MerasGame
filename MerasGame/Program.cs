using System;
using MerasGame.Models;
using MerasGame.Managers;

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
            
            var city = new City(name);

            int rounds = InputHelper.PromptInt("How many rounds do you need to survive to win? (5-100)", 5, 100);

            var game = new Game(city, rounds);
            game.Run();
        }
    }
}