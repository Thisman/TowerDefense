using UnityEngine;
using Zenject;

public class EnemiesInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<EnemiesSpawnModel>().AsSingle();
    }
}
