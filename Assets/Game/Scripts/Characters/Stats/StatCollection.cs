using Game.Characters.Stats.Commons;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Game.Characters.Stats
{
    /// <summary>
    /// Stat collection wrapper
    /// </summary>
    public sealed class StatCollection
    {
        public readonly IReadOnlyDictionary<EStat, Stat> StatDictionary;
        public readonly IReadOnlyCollection<IStatModifierProvider> Sources;

        readonly List<IStatModifierProvider> _sources;
        readonly Dictionary<EStat, Stat> _statDictionary;
        readonly HashSet<IStatModifierProvider> _sourcesMap;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stats">Instances that will be used</param>
        public StatCollection(params Stat[] stats)
        {
            _statDictionary = stats.ToDictionary(x => x.Type, x => x);
            _sourcesMap = new HashSet<IStatModifierProvider>();
            _sources = new List<IStatModifierProvider>();

            StatDictionary = new ReadOnlyDictionary<EStat, Stat>(_statDictionary);
            Sources = _sources.AsReadOnly();
        }

        /// <summary>
        /// Will not use provided referense or modifiers. Only used to copy base values
        /// </summary>
        public void SetValues(params Stat[] stats)
        {
            foreach (var stat in stats)
            {
                var cachedStat = _statDictionary[stat.Type];
                cachedStat.SetValues(stat.BaseValue, stat.MaxValue);
            }            
        }

        public float GetValue(EStat type)
        {
            return _statDictionary[type].Value;
        }

        /// <summary>
        /// Provides readonly stat class
        /// </summary>
        public Stat GetStat(EStat type)
        {
            return StatDictionary[type];
        }

        public void ApplyModifiers(params IStatModifierProvider[] providers)
        {
            foreach (var provider in providers)
            {
                if (!IsAffectedBy(provider))
                {
                    foreach (var item in provider.GetModifiers())
                    {
                        _statDictionary[item.Key].AddModifiers(item.Value);
                    }

                    _sources.Add(provider);
                    _sourcesMap.Add(provider);
                }
            }
        }

        public void RemoveModifiers(params IStatModifierProvider[] providers)
        {
            foreach (var provider in providers)
            {
                if (IsAffectedBy(provider))
                {
                    foreach (var item in provider.GetModifiers())
                    {
                        _statDictionary[item.Key].RemoveModifiers(item.Value);
                    }

                    _sources.Remove(provider);
                    _sourcesMap.Remove(provider);
                }
            }
        }

        public bool IsAffectedBy(IStatModifierProvider provider)
        {
            return _sourcesMap.Contains(provider);
        }
    }
}