using Game.Characters.Stats;
using System;
using UnityEngine;

namespace Game.Characters
{
    [Serializable]
    public class CharacterSettings
    {
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public StatCollection Stats { get; private set; }
    }
}