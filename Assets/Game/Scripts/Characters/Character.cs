using UnityEngine;

namespace Game.Characters
{
    public abstract class Character
    {
        public GameObject View { get; }
        public Stats Stats { get; }

        public Character(CharacterSettings settings)
        {
            View = GameObject.Instantiate(settings.Prefab);
            Stats = settings.Stats;
        }

        public Character(Stats stats)
        {
            Stats = stats;
            View = new GameObject("Dummy");
        }
    }
}
