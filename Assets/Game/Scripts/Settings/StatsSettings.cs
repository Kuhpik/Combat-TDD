using Game.Characters.CharacterStats;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Settings
{
    [CreateAssetMenu(menuName = "Game/Stats")]
    public class StatsSettings : ScriptableObject
    {
        [SerializeField] Stat[] _stats;

        public IReadOnlyCollection<Stat> Stats => _stats;
    }
}