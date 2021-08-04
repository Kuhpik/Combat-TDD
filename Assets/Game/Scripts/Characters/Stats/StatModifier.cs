using Game.Characters.CharacterStats.Commons;

namespace Game.Characters.CharacterStats
{
    public class StatModifier
    {
        public readonly EBonusType Type;
        public readonly float Value;

        public StatModifier(EBonusType type, float value)
        {
            Type = type;
            Value = value;
        }
    }
}