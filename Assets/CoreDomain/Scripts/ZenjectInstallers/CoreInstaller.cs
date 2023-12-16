using CoreDomain.Scripts.Services.DataPersistence;
using CoreDomain.Services;
using CoreDomain.Services.GameStates;
using Handlers.Serializers.Serializer;
using Services.Logs;
using UnityEngine;
using Zenject;

namespace CoreDomain
{
    public class CoreInstaller : MonoInstaller
    {
        [SerializeField] private UpdateSubscriptionService _updateSubscriptionService;
        [SerializeField] private AudioService _audioService;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<UnityLogger>().AsSingle().NonLazy();
            Container.BindInterfacesTo<CameraService>().AsSingle().NonLazy();
            Container.BindInterfacesTo<SceneLoaderService>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ResourcesLoaderService>().AsSingle().NonLazy();
            Container.BindInterfacesTo<StateMachineService>().AsSingle().NonLazy();
            Container.BindInterfacesTo<UpdateSubscriptionService>().FromInstance(_updateSubscriptionService).AsSingle().NonLazy();
            Container.BindInterfacesTo<AudioService>().FromInstance(_audioService).AsSingle().NonLazy();
            Container.BindInterfacesTo<SerializerService>().AsSingle().NonLazy();
            Container.BindInterfacesTo<PlayerPrefsDataPersistence>().AsSingle().NonLazy();
            Container.Bind<GameInputActions>().AsSingle().NonLazy();
        }
    }
}
