using System;
using System.Collections.Generic;
using MerasGame.Models;

namespace MerasGame.Managers
{
    public class EventProcessor
    {
        private const double RANDOM_EVENT_CHANCE = 0.25;

        private readonly List<CityEvent> _events;
        private readonly Random _rng;

        public EventProcessor(List<CityEvent> events, Random rng)
        {
            _events = events;
            _rng = rng;
        }

        public void ProcessRandomEvents(City city)
        {
            Console.WriteLine("\n=== Random Events ===");
            
            if (city.IsLost)
            {
                return;
            }

            if (_rng.NextDouble() < RANDOM_EVENT_CHANCE)
            {
                var ev = _events[_rng.Next(_events.Count)];
                Console.WriteLine($"{city.Name}: {ev.Name} - {ev.Description}");
                city.ApplyRandomEvent(ev);
            }
            else
            {
                Console.WriteLine($"{city.Name}: No significant events.");
            }
        }
    }
}
