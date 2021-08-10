using System.Collections.Generic;

namespace Game.Characters.Stats.Commons
{
    public interface IStatModifierProvider
    {
        IReadOnlyDictionary<EStat, IReadOnlyCollection<StatModifier>> GetModifiers();
    }
}