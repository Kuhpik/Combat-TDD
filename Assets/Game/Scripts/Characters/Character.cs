using Game.Characters.Teams;
using UnityEngine;
using Game.Characters.CharacterStats;

namespace Game.Characters
{
    public abstract class Character
    {
        public GameObject View { get; }
        public Stats Stats { get; }
        public Team Team { get; private set; }

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

        public void SetTeam(Team team)
        {
            Team = team;
        }
    }
}
