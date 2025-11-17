using MerasGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MerasGame.Managers
{
    public class SpecialEventProcessor
    {
        private readonly List<SpecialEvent> _specialEvents;

        public SpecialEventProcessor(List<SpecialEvent> specialEvents)
        {
            _specialEvents = specialEvents;
        }

        public void CheckAndApplySpecialEvents(City city)
        {
            if (city.IsLost)
            {
                return;
            }

            foreach (var specialEvent in _specialEvents)
            {
                specialEvent.CheckCondition(city);

                if (specialEvent.IsActive && specialEvent.WasTriggered)
                {
                    ShowSpecialEventActivation(city, specialEvent);
                }

                if (specialEvent.IsActive)
                {
                    specialEvent.Execute(city);
                }
            }
        }

        public void ShowActiveSpecialEvents(City city)
        {
            var activeEvents = _specialEvents.Where(e => 
            {
                e.CheckCondition(city);
                return e.IsActive;
            }).ToList();

            if (activeEvents.Any())
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($" SPECIAL EVENTS ({city.Name}):");
                foreach (var ev in activeEvents)
                {
                    Console.WriteLine($"   • {ev.Name}: {ev.Description}");
                }
                Console.ResetColor();
                Console.WriteLine();
            }
        }

        private void ShowSpecialEventActivation(City city, SpecialEvent specialEvent)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n  SPECIAL EVENT ACTIVATED: {city.Name}");
            Console.WriteLine($"   {specialEvent.Name}");
            Console.WriteLine($"   {specialEvent.Description}");
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}
