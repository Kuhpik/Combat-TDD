using Game.Characters.Stats.Factories;
using Zenject;

namespace Game.Characters
{
    public class Warrior : Character
    {
        public Warrior(IStatCollectionFactory statsFactory, CharacterSettings settings = null) 
            : base(statsFactory, settings) { }

        public class Factory : PlaceholderFactory<Warrior> { }
    }
}