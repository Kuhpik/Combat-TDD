using Game.Characters.CharacterStats.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Characters.CharacterStats.Utils
{
    //We don't need abstraction right now
    public class StatCalculator
    {
        readonly Dictionary<EBonusType, float> _modifierValues;

        public StatCalculator()
        {
            _modifierValues = (Enum.GetValues(typeof(EBonusType)) as IList<EBonusType>)
                .ToDictionary(x => x, x => 0f);
        }

        public float GetValue(float baseValue, float maxValue, IReadOnlyCollection<StatModifier> modifiers)
        {
            var value = baseValue;

            ResetValues();
            FillValues(modifiers);

            value *= _modifierValues[EBonusType.MultiplyBase] + 1; //Percents
            value += _modifierValues[EBonusType.Flat];
            value *= _modifierValues[EBonusType.Multiply] + 1; //Percents
            value += _modifierValues[EBonusType.UnscaledFlat];
            value = Mathf.Round(value * 100) / 100;

            return maxValue == -1 ? value : Mathf.Clamp(value, 0, maxValue);
        }

        void FillValues(IReadOnlyCollection<StatModifier> modifiers)
        {
            foreach (var modifier in modifiers)
            {
                _modifierValues[modifier.Type] += modifier.Value;
            }
        }

        void ResetValues()
        {
            foreach (var key in _modifierValues.Keys.ToArray())
            {
                _modifierValues[key] = 0;
            }
        }
    }
}