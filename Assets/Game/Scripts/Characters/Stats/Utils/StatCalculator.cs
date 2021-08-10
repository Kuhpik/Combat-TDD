using Game.Characters.Stats.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Characters.Stats.Utils
{
    public class StatCalculator
    {
        readonly Dictionary<EBonusType, float> _modifierValues;

        /// <summary>
        /// Will create dictionary will all EBonusType values available
        /// </summary>
        public StatCalculator()
        {
            _modifierValues = (Enum.GetValues(typeof(EBonusType)) as IList<EBonusType>)
                .ToDictionary(x => x, x => 0f);
        }

        /// <summary>
        /// Will create dictionary with specified EBonusType values
        /// </summary>
        public StatCalculator(IEnumerable<EBonusType> types)
        {
            _modifierValues = types.ToDictionary(x => x, x => 0f);
        }

        public float GetValue(float baseValue, float maxValue, IEnumerable<StatModifier> modifiers)
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

        void FillValues(IEnumerable<StatModifier> modifiers)
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