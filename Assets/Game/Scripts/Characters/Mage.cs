using Game.Characters.CharacterStats;
using Zenject;

namespace Game.Characters
{
    public class Mage : Character
    {
        public Mage(CharacterSettings settings) : base(settings) { }
        public Mage(Stats stats) : base(stats) { } 

        public class Factory : PlaceholderFactory<Mage> { }
    }
}