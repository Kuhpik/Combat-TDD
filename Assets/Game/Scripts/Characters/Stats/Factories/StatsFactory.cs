using Game.Characters.Stats.Commons;
using Game.Characters.Stats.Utils;
using System;

namespace Game.Characters.Stats.Factories
{
    //Not using IFactory interface cause I don't want to heavily depend on Zenject
    public class StatsFactory : IStatsFactory
    {
        readonly StatCalculator _statCalculator;

        public StatsFactory(StatCalculator statCalculator)
        {
            _statCalculator = statCalculator;
        }

        public Stat[] Create()
        {
            var statTypesInGame = Enum.GetValues(typeof(EStat)) as EStat[];
            var stats = new Stat[statTypesInGame.Length];

            for (int i = 0; i < stats.Length; i++)
            {
                var type = statTypesInGame[i];
                stats[i] = new Stat(_statCalculator, type);
            }

            return stats;
        }
    }
}