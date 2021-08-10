using Game.Characters.Stats.Commons;
using Game.Settings;

namespace Game.Characters.Stats.Factories
{
    //Not using IFactory interface cause I don't want to heavily depend on Zenject
    public class StatCollectionFactory : IStatCollectionFactory
    {
        const float _maxHP = 10000;
        const float _maxStat = 999;

        readonly StatsSettings _settings;
        readonly IStatsFactory _statsFactory;

        public StatCollectionFactory(StatsSettings settings, IStatsFactory statsFactory)
        {
            _settings = settings;
            _statsFactory = statsFactory;
        }

        public StatCollection Create(Character character)
        {
            var characterStats = _statsFactory.Create();
            var statCollection = new StatCollection(characterStats);

            statCollection.SetValues(_settings.Stats as Stat[]);
            HandleClassBasedValues(character, statCollection);

            return statCollection;
        }

        void HandleClassBasedValues(Character character, StatCollection statCollection)
        {
            if (character is Mage)
            {
                statCollection.GetStat(EStat.AbilityPower).SetValues(10, _maxStat);
            }

            else if (character is Warrior)
            {
                statCollection.GetStat(EStat.Health).SetValues(300, _maxHP);
            }
        }
    }
}
