using Game.Core;
using UnityEngine;
using Zenject;

public class CoreInstaller : MonoInstaller
{
    [SerializeField]
    private MusicController _musicController;

    public override void InstallBindings()
    {
        Container.Bind<FSM>().AsSingle();
        Container.Bind<MusicController>().FromInstance(_musicController);
    }
}
