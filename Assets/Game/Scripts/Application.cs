using Game.Characters;
using Game.Characters.Stats.Commons;
using UnityEngine;
using Zenject;

namespace Game
{
    public class Application : ITickable
    {
        Mage.Factory _mageFactory;
        Warrior.Factory _warriorFactory;

        public Application(Mage.Factory mageFactory, Warrior.Factory warriorFactory)
        {
            _mageFactory = mageFactory;
            _warriorFactory = warriorFactory;
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var mage = _mageFactory.Create();
                var warrior = _warriorFactory.Create();

                Debug.Log($"Mage's health: {mage.Stats.GetValue(EStat.Health)}.");
                Debug.Log($"Warrior's health: {warrior.Stats.GetValue(EStat.Health)}");
            }
        }
    }
}
