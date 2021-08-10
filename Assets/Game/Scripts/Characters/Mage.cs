using Game.Characters.Stats.Factories;
using Zenject;

namespace Game.Characters
{
    public class Mage : Character
    {
        public Mage(IStatCollectionFactory statsFactory, CharacterSettings settings = null) 
            : base(statsFactory, settings) { }

        public class Factory : PlaceholderFactory<Mage> { }
    }
}