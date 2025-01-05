using Game.States;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameFSM>().AsSingle();
        Container.Bind<PlayerFSM>().AsSingle();
    }
}