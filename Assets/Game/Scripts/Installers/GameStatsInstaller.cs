using Game.Characters.Stats.Factories;
using Game.Characters.Stats.Utils;
using Game.Settings;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class GameStatsInstaller : MonoInstaller
    {
        [SerializeField] StatsSettings _settings;

        public override void InstallBindings()
        {
            if (_settings == null)
            {
                Debug.LogError("Settings was not set");
                return;
            }

            Container.BindInstance(_settings);
            Container.Bind<StatCalculator>().ToSelf().AsTransient();
            Container.Bind<IStatsFactory>().To<StatsFactory>().AsSingle();
            Container.Bind<IStatCollectionFactory>().To<StatCollectionFactory>().AsSingle();
        }
    }
}