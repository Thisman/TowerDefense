using UnityEngine;
using Zenject;

public class CastleInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<CastleModel>().AsSingle();
    }
}