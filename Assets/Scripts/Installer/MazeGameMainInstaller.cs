using Controller;
using Model;
using Redbus;
using Redbus.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;
using View;
using View.Components;
using Zenject;

namespace Installer
{
    public class MazeGameMainInstaller : MonoInstaller
    {
        [SerializeField] private MazeGameView mazeGameView;
        [SerializeField] private Checkpoint checkpoint;
        public override void InstallBindings()
        {
            Container.Bind<IEventBus>().To<EventBus>().AsSingle();
            Container.Bind<MazeGameMazeModel>().AsTransient();
            Container.Bind<MazeGameBallModel>().AsTransient();
            Container.Bind<Checkpoint>().FromInstance(checkpoint).AsTransient();
            Container.Bind<MazeGameView>().FromInstance(mazeGameView).NonLazy();
            Container.Bind<MazeGameController>().AsSingle().NonLazy();
        }
    }
}
