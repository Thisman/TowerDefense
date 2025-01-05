using Game.Map;
using UnityEngine;
using Zenject;

public class MapInstaller : MonoInstaller
{
    [SerializeField]
    private MapModel _mapModelInstance;

    public override void InstallBindings()
    {
        Container.Bind<MapModel>().FromInstance(_mapModelInstance);

        Container.Bind<MapBuilder>().AsSingle();
        Container.Bind<MapPathFinder>().AsSingle();
        Container.Bind<MapHighlighter>().AsSingle();
    }
}
