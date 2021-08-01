using Zenject;

namespace Game.Characters
{
    public class Warrior : Character
    {
        public Warrior(CharacterSettings settings) : base(settings) { }
        public class Factory : PlaceholderFactory<Warrior> { }
    }
}