using Game.Characters;
using Game.Characters.Stats.Factories;
using Game.Characters.Stats.Utils;
using Game.Settings;
using System.Collections.Generic;
using UnityEngine;

namespace Tests.Helpers
{
    public class TestCharactersCreator
    {
        readonly StatsFactory _statsFactory;
        readonly StatCollectionFactory _statCollectionFactory;
        readonly StatsSettings _statsSettings;

        public TestCharactersCreator()
        {
            _statsSettings = ScriptableObject.CreateInstance<StatsSettings>();
            _statsFactory = new StatsFactory(new StatCalculator());
            _statCollectionFactory = new StatCollectionFactory(_statsSettings, _statsFactory);
        }

        public Character CreateCharacter()
        {
            Character character = new Warrior(_statCollectionFactory);

            return character;
        }

        public List<Character> CreateCharacters(int count)
        {
            List<Character> characters = new List<Character>(count);

            for (int i = 0; i < count; i++)
            {
                characters.Add(CreateCharacter());
            }

            return characters;
        }
    }
}
