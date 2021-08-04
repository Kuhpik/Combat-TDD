using System.Collections.Generic;

namespace Game.Characters.CharacterStats.Commons
{
    public interface IStatModifierProvider
    {
        IReadOnlyDictionary<EStat, IReadOnlyCollection<StatModifier>> GetModifiers();
    }
}