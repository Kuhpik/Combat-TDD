using Game.Characters.Stats.Commons;

namespace Game.Characters.Stats
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