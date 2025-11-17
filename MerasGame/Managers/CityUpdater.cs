using MerasGame.Models;
using System.Collections.Generic;

namespace MerasGame.Managers
{
    public class CityUpdater
    {
        private readonly List<CityCondition> _conditions;

        public CityUpdater(List<CityCondition> conditions)
        {
            _conditions = conditions;
        }

        public void UpdateCity(City city)
        {
            if (city.IsLost)
            {
                return;
            }

            city.UpdateConditions(_conditions);
            
            city.ApplyActiveConditions();
            
            city.EndOfRoundUpdate();
        }
    }
}
