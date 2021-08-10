using Game.Characters.Teams;
using UnityEngine;
using Game.Characters.Stats;
using Game.Characters.Stats.Complex;
using Game.Characters.Stats.Factories;

namespace Game.Characters
{
    public abstract class Character
    {
        public GameObject View { get; }
        public StatCollection Stats { get; }
        public Team Team { get; private set; }

        public readonly Health Health;

        public Character(IStatCollectionFactory statsFactory, CharacterSettings settings = null)
        {
            View = settings != null ? GameObject.Instantiate(settings.Prefab) : new GameObject("Dummy");
            Stats = statsFactory.Create(this);
            Health = new Health(Stats);
        }

        public void SetTeam(Team team)
        {
            Team = team;
        }
    }
}
