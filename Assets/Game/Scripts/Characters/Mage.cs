using Zenject;

namespace Game.Characters
{
    public class Mage : Character
    {
        public Mage(CharacterSettings settings) : base(settings) { }
        public class Factory : PlaceholderFactory<Mage> { }
    }
}