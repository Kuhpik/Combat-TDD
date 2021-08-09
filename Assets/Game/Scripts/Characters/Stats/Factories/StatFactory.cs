using Game.Characters.CharacterStats.Commons;
using Game.Characters.CharacterStats.Utils;
using Game.Settings;
using System.Collections.Generic;
using System.Linq;

namespace Game.Characters.CharacterStats.Factories
{
    public class StatFactory : IStatFactory
    {
        readonly StatsSettings _settings;
        readonly StatCalculator _statCalculator;

        const float _maxHP = 10000;
        const float _maxStat = 999;

        public StatFactory(StatsSettings settings, StatCalculator statCalculator)
        {
            _settings = settings;
            _statCalculator = statCalculator;
        }

        public Stats GetStats(Character character)
        {
            var stats = new Stats(_settings.Stats as Stat[]);

            if (character is Mage)
            {
                stats.GetStat(EStat.AbilityPower).SetValues(10, _maxStat);
            }

            else if (character is Warrior)
            {
                stats.GetStat(EStat.Health).SetValues(300, _maxHP);
            }

            return stats;
        }
    }
}
