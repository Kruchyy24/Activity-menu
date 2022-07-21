using Controller;
using Model;
using Redbus;
using Redbus.Interfaces;
using Service;
using UnityEngine;
using View;
using Zenject;

namespace Installer
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField]
        private ActivityView activityView;
    
        [SerializeField]
        private CoroutineService coroutineService;

        [SerializeField]
        private LoadingView loadingView;
        public override void InstallBindings()
        {
            Container.Bind<LoadingView>().FromInstance(loadingView).NonLazy();
            Container.Bind<ActivityView>().FromInstance(activityView);
            Container.Bind<CoroutineService>().FromComponentInNewPrefab(coroutineService).AsSingle();
            Container.Bind<ActivityModel>().AsTransient();
            Container.Bind<IBoredService>().To<BoredService>().AsSingle();
            Container.Bind<ActivityController>().AsSingle().NonLazy();
            Container.Bind<IEventBus>().To<EventBus>().AsSingle();
        }
    }
}
