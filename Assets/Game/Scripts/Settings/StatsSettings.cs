using Game.Characters.Stats;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Settings
{
    [CreateAssetMenu(menuName = "Game/Stats")]
    public class StatsSettings : ScriptableObject
    {
        [SerializeField] Stat[] _stats;
        public IReadOnlyCollection<Stat> Stats => _stats;

        //Quick fix for mocking purposes
        public StatsSettings()
        {
            _stats = new Stat[0];
        }
    }
}