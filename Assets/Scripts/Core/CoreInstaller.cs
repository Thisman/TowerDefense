using Game.Core;
using UnityEngine;
using Zenject;

public class CoreInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<FSM>().AsSingle();
    }
}
