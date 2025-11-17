using System;
using MerasGame.Models.Effects;

namespace MerasGame.Models
{
    public class CityEvent : CityEffect
    {
        private readonly Action<City> _effect;

        public CityEvent(string name, string description, Action<City> effect) 
            : base(name, description)
        {
            _effect = effect;
        }

        protected override void ApplyEffect(City city)
        {
            _effect?.Invoke(city);
        }
    }
}