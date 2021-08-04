using Game.Characters.CharacterStats.Commons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Game.Characters.CharacterStats
{
    /// <summary>
    /// Stat holder
    /// </summary>
    public class Stats
    {
        public readonly ReadOnlyDictionary<EStat, Stat> StatDictionary;
        readonly Dictionary<EStat, Stat> _statDictionary;

        public Stats()
        {
            _statDictionary = (Enum.GetValues(typeof(EStat)) as IList<EStat>)
                .ToDictionary(x => x, x => new Stat());

            StatDictionary = new ReadOnlyDictionary<EStat, Stat>(_statDictionary);
        }
    }
}