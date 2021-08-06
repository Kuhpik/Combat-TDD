using Game.Characters.CharacterStats.Commons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Game.Characters.CharacterStats
{
    /// <summary>
    /// Stat collection wrapper
    /// </summary>
    public sealed class Stats
    {
        public readonly IReadOnlyDictionary<EStat, Stat> StatDictionary;
        public readonly IReadOnlyCollection<IStatModifierProvider> Sources;

        readonly List<IStatModifierProvider> _sources;
        readonly Dictionary<EStat, Stat> _statDictionary;
        readonly HashSet<IStatModifierProvider> _sourcesMap;

        public Stats()
        {
            _statDictionary = (Enum.GetValues(typeof(EStat)) as IList<EStat>)
                .ToDictionary(x => x, x => new Stat());

            StatDictionary = new ReadOnlyDictionary<EStat, Stat>(_statDictionary);
            _sourcesMap = new HashSet<IStatModifierProvider>();
            _sources = new List<IStatModifierProvider>();
            Sources = _sources.AsReadOnly();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stats">Will not use provided references or modifiers. Only used to copy base values.</param>
        public Stats(IEnumerable<Stat> stats) : this()
        {
            foreach (var stat in stats)
            {
                SetStat(stat);
            }
        }

        /// <summary>
        /// Will not use provided referense or modifiers. Only used to copy base values
        /// </summary>
        public void SetStat(Stat stat)
        {
            var cachedStat = _statDictionary[stat.Type];
            cachedStat.SetValues(stat.BaseValue, stat.MaxValue);
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