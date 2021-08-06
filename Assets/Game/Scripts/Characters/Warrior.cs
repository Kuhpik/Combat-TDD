using Game.Characters.CharacterStats;
using Zenject;

namespace Game.Characters
{
    public class Warrior : Character
    {
        public Warrior(CharacterSettings settings) : base(settings) { }
        public Warrior(Stats stats) : base(stats) { }

        public class Factory : PlaceholderFactory<Warrior> { }
    }
}