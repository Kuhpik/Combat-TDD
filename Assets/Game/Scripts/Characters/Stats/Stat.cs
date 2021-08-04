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

        public readonly EStat Type;
        public readonly IReadOnlyCollection<StatModifier> Modifiers;

        float _value;
        bool _wasModified = true; //In most cases value != baseValue at the start

        readonly StatCalculator _calculator;
        readonly List<StatModifier> _modifiers;

        readonly Dictionary<IStatModifierProvider, IReadOnlyCollection<StatModifier>> _modifierProviders;
        readonly Dictionary<IStatModifierProvider, Vector2Int> _modifierCollectionIndexes;

        public Stat()
        {
            _modifierProviders = new Dictionary<IStatModifierProvider, IReadOnlyCollection<StatModifier>>();
            _modifierCollectionIndexes = new Dictionary<IStatModifierProvider, Vector2Int>();
            _modifiers = new List<StatModifier>();
            Modifiers = _modifiers.AsReadOnly();
        }

        public Stat(StatCalculator calculator, EStat type, float baseValue, float maxValue) : this()
        {
            Type = type;
            _calculator = calculator;
            SetValues(baseValue, maxValue);
        }

        public void SetValues(float baseValue, float maxValue)
        {
            _maxValue = maxValue;
            _baseValue = baseValue;
        }

        public void AddModifiers(IReadOnlyCollection<StatModifier> modifiers, IStatModifierProvider provider)
        {
            if (!_modifierProviders.ContainsKey(provider))
            {
                _modifierCollectionIndexes.Add(provider, new Vector2Int(_modifiers.Count - 1, modifiers.Count));
                _modifierProviders.Add(provider, modifiers);
                _modifiers.AddRange(modifiers);

                _wasModified = true;
            }
        }

        public void RemoveModifiers(IStatModifierProvider provider)
        {
            if (_modifierProviders.ContainsKey(provider))
            {
                var indexes = _modifierCollectionIndexes[provider];

                _modifierProviders.Remove(provider);
                _modifierCollectionIndexes.Remove(provider);
                _modifiers.RemoveRange(indexes.x, indexes.y);

                _wasModified = true;
            }
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