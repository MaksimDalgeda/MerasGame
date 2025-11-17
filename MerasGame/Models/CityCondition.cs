using System;
using MerasGame.Models.Effects;

namespace MerasGame.Models
{
    public class CityCondition : CityEffect
    {
        private readonly Func<City, bool> _condition;
        private readonly Action<City> _effect;

        public bool IsActive { get; private set; }

        public CityCondition(string name, string description, 
            Func<City, bool> condition, Action<City> effect) 
            : base(name, description)
        {
            _condition = condition ?? throw new ArgumentNullException(nameof(condition));
            _effect = effect ?? throw new ArgumentNullException(nameof(effect));
            IsActive = false;
        }

        public void UpdateStatus(City city)
        {
            if (city == null || city.IsLost)
            {
                IsActive = false;
                return;
            }

            bool shouldBeActive = _condition(city);
            
            if (shouldBeActive != IsActive)
            {
                IsActive = shouldBeActive;
            }
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
