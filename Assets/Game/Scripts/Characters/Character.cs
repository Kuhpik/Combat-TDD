using Game.Characters.Teams;
using UnityEngine;
using Game.Characters.CharacterStats;
using Game.Characters.CharacterStats.Complex;

namespace Game.Characters
{
    public abstract class Character
    {
        public GameObject View { get; }
        public Stats Stats { get; }
        public Team Team { get; private set; }

        public readonly Health Health;

        public Character(CharacterSettings settings)
        {
            View = GameObject.Instantiate(settings.Prefab);
            Health = new Health(settings.Stats);
            Stats = settings.Stats;
        }

        public Character(Stats stats)
        {
            Stats = stats;
            Health = new Health(stats);
            View = new GameObject("Dummy");
        }

        public void SetTeam(Team team)
        {
            Team = team;
        }
    }
}
