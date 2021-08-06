using Game.Characters.CharacterStats.Commons;
using Game.Characters.CharacterStats.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters.CharacterStats
{
    [Serializable]
    public class Stat
    {
        [SerializeField] float _baseValue;
        [SerializeField] float _maxValue = -1;

        public float Value => GetValue();
        public float MaxValue => _maxValue;
        public float BaseValue => _baseValue;

        public readonly EStat Type;
        public readonly IReadOnlyCollection<StatModifier> Modifiers;

        float _value;
        bool _wasModified;
        readonly StatCalculator _calculator;
        readonly List<StatModifier> _modifiers;

        public Stat()
        {
            _modifiers = new List<StatModifier>();
            Modifiers = _modifiers.AsReadOnly();
        }

        public Stat(StatCalculator calculator, EStat type) : this()
        {
            Type = type;
            _calculator = calculator;
        }

        public Stat(StatCalculator calculator, EStat type, float baseValue, float maxValue) : this(calculator, type)
        {
            SetValues(baseValue, maxValue);
        }

        internal void SetValues(float baseValue, float maxValue)
        {
            _maxValue = maxValue;
            _baseValue = baseValue;
            _wasModified = true;
        }

        internal void AddModifiers(IEnumerable<StatModifier> modifiers)
        {
            _modifiers.AddRange(modifiers);
            _wasModified = true;
        }

        internal void RemoveModifiers(IEnumerable<StatModifier> modifiers)
        {
            foreach (var modifier in modifiers)
            {
                _modifiers.Remove(modifier);
            }

            _wasModified = true;
        }

        void Recalculate()
        {
            _wasModified = false;
            _value = _calculator.GetValue(_baseValue, _maxValue, _modifiers);
        }

        //Optimized way.
        //In case many modifiers will be added at the same time.
        //So we're not calculating Value until player request it.
        float GetValue()
        {
            if (_wasModified) Recalculate();
            return _value;
        }
    }
}