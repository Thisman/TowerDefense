using UnityEngine;
using Zenject;

public class BuilderInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<BuilderModel>().AsSingle();
        Container.Bind<BuilderBankModel>().AsSingle();
    }
}
