using UnityEngine;
using Zenject;

public class MapInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<MapModel>().AsSingle();

        Container.Bind<MapPathFinder>().AsSingle();
        Container.Bind<MapHighlighter>().AsSingle();
        Container.Bind<MapTowerBuilder>().AsSingle();
        Container.Bind<MapTerraformer>().AsSingle();
    }
}
