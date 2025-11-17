using System;
using MerasGame.Models.Effects;

namespace MerasGame.Models
{
    public class SpecialEvent : CityEffect
    {
        private readonly Func<City, bool> _condition;
        private readonly Action<City> _effect;

        public bool IsActive { get; private set; }
        public bool WasTriggered { get; private set; }

        public SpecialEvent(string name, string description, 
            Func<City, bool> condition, Action<City> effect) 
            : base(name, description)
        {
            _condition = condition ?? throw new ArgumentNullException(nameof(condition));
            _effect = effect ?? throw new ArgumentNullException(nameof(effect));
            IsActive = false;
            WasTriggered = false;
        }

        public void CheckCondition(City city)
        {
            if (city == null || city.IsLost)
            {
                IsActive = false;
                return;
            }

            bool shouldBeActive = _condition(city);
            
            if (shouldBeActive && !WasTriggered)
            {
                WasTriggered = true;
            }

            IsActive = shouldBeActive;
        }

        protected override void ApplyEffect(City city)
        {
            if (IsActive)
            {
                _effect?.Invoke(city);
            }
        }
    }
}
