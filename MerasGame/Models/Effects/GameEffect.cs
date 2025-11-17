using System;

namespace MerasGame.Models.Effects
{
    public abstract class GameEffect
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }

        protected GameEffect(string name, string description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        public void Execute(City city)
        {
            if (!CanExecute(city))
            {
                return;
            }

            ApplyEffect(city);
        }

        protected virtual bool CanExecute(City city)
        {
            return city != null && !city.IsLost;
        }

        protected abstract void ApplyEffect(City city);
    }
}
