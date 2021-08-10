using Game.Characters.Stats.Commons;
using System;
using UnityEngine;

namespace Game.Characters.Stats.Complex
{
    [Serializable]
    public class Health
    {
        [SerializeField] float _value;
        [SerializeField] float _maxValue;

        public float Value => GetValue();
        public float MaxValue => _maxValue;

        readonly StatCollection _playerStats;

        public Health(StatCollection stats)
        {
            _playerStats = stats;
            GetValue();
        }

        public void Reduce(float value)
        {
            _value = Mathf.Clamp(_value - value, 0, _maxValue);
        }

        public void Restore(float value)
        {
            _value = Mathf.Clamp(_value + value, 0, _maxValue);
        }

        float GetValue()
        {
            var value = _playerStats.GetValue(EStat.Health);
            ChangeMaxValue(value);
            return _value;
        }

        void ChangeMaxValue(float value)
        {
            if (_value == _maxValue || //Value as not changed
                value < _value) //New max value is even lower than our value 
            {
                _value = _maxValue = value;
            }

            else
            {
                _maxValue = value;
            }
        }
    }
}