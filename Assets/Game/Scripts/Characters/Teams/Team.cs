using System.Collections.Generic;

namespace Game.Characters.Teams
{
    public class Team
    {
        public readonly int ID;
        public string Name { get; private set; }
        public readonly IReadOnlyCollection<Team> Allies;
        public readonly IReadOnlyCollection<Team> Enemies;
        public readonly IReadOnlyCollection<Character> Members;

        readonly List<Team> _allies;
        readonly List<Team> _enemies;
        readonly List<Character> _members;

        public Team(int id)
        {
            ID = id;

            _allies = new List<Team>();
            _enemies = new List<Team>();
            _members = new List<Character>();

            Allies = _allies.AsReadOnly();
            Enemies = _enemies.AsReadOnly();
            Members = _members.AsReadOnly();
        }

        public Team(int id, string name) : this(id)
        {
            SetName(name);
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void AddMembers(params Character[] characters)
        {
            _members.AddRange(characters);
        }

        public void AddAllies(params Team[] allies)
        {
            _allies.AddRange(allies);
        }

        public void AddEnemies(params Team[] enemies)
        {
            _enemies.AddRange(enemies);
        }

        public void RemoveMembers(params Character[] charactersToRemove)
        {
            foreach (var member in charactersToRemove)
            {
                _members.Remove(member);
            }
        }

        public void RemoveAllies(params Team[] alliesToRemove)
        {
            foreach (var ally in alliesToRemove)
            {
                _allies.Remove(ally);
            }
        }

        public void RemoveEnemies(params Team[] enemiesToRemove)
        {
            foreach (var enemy in enemiesToRemove)
            {
                _enemies.Remove(enemy);
            }
        }
    }
}
