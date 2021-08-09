using Game.Characters;
using Game.Characters.CharacterStats;
using Game.Settings;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class GameStatsInstaller : MonoInstaller
    {
        [SerializeField] Stats _statsSettings;

        public override void InstallBindings()
        {
            if (_statsSettings == null)
            {
                Debug.LogError("Settings was not set");
                return;
            }

            //Container.BindFactory<Mage, Mage.Factory>().WithArguments(_statsSettings.Mage);
            //Container.BindFactory<Warrior, Warrior.Factory>().WithArguments(_statsSettings.Warrior);

            Container.BindInstance(_statsSettings);
        }
    }
}