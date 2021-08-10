using Game.Characters;
using Game.Settings;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class GameSettingsInstaller : MonoInstaller
    {
        [SerializeField] GameSettings _settings;

        public override void InstallBindings()
        {
            if (_settings == null)
            {
                Debug.LogError("Settings was not set");
                return;
            }

            Container.BindInstance(_settings);
            Container.BindFactory<Mage, Mage.Factory>().WithArguments(_settings.Mage);
            Container.BindFactory<Warrior, Warrior.Factory>().WithArguments(_settings.Warrior);
        }
    }
}