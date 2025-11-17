using System;

namespace MerasGame.Managers
{
    public static class InputHelper
    {
        public static void PressAnyKeyToContinue()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }

        public static int ReadChoice(int min, int max)
        {
            while (true)
            {
                Console.Write($"Choose [{min}-{max}]: ");
                var s = Console.ReadLine();
                if (int.TryParse(s, out var v) && v >= min && v <= max) return v;
                Console.WriteLine("Invalid choice, try again.");
            }
        }

        public static int PromptInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                Console.Write($"> ");
                var s = Console.ReadLine();
                if (int.TryParse(s, out var v) && v >= min && v <= max) return v;
                Console.WriteLine($"Enter a number between {min} and {max}.");
            }
        }
    }
}
