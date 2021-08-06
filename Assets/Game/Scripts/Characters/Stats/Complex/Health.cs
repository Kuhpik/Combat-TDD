using Game.Characters.CharacterStats.Commons;
using System;
using UnityEngine;

namespace Game.Characters.CharacterStats.Complex
{
    [Serializable]
    public class Health
    {
        [SerializeField] float _value;
        [SerializeField] float _maxValue;

        /// <summary>
        /// Used to track changes
        /// Lets say player had 8\10 hp. If we increase 'Health' stat value by 10 we end up 18\20
        /// </summary>
        float _changedValue;

        public float Value => _value + _changedValue;
        public float MaxValue => _value;

        readonly Stats _playerStats;

        public Health(Stats stats)
        {
            _playerStats = stats;
            ChangeMaxValue(stats.GetValue(EStat.Health));
            _playerStats.GetStat(EStat.Health).SubscribeToValueChangeEvent(ChangeMaxValue);
        }

        public void Reduce(float value)
        {
            _changedValue -= value;

            if (Value <= 0)
            {
                _value = 0;
                _changedValue = 0;
            }
        }

        public void Restore(float value)
        {
            _changedValue += value;

            if (Value > _maxValue)
            {
                _value = _maxValue;
                _changedValue = 0;
            }
        }

        void ChangeMaxValue(float value)
        {
            if (value <= 0) return;

            _maxValue = value;
            _value = _maxValue + _changedValue;

            if (_value > _maxValue)
            {
                _value = _maxValue;
            }
        }
    }
}