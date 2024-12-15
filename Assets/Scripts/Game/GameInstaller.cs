using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameTimeModel>().AsSingle();
        Container.Bind<GameStateModel>().AsSingle();
    }
}