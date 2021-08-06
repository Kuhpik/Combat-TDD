using Game.Characters.CharacterStats;
using System;
using UnityEngine;

namespace Game.Characters
{
    [Serializable]
    public class CharacterSettings
    {
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public Stats Stats { get; private set; }
    }
}