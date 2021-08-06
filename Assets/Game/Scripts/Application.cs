using Game.Characters;
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
                Debug.Log($"Mage's health: {mage.Health.Value}.");
            }
        }
    }
}
