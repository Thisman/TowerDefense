using Game.Map;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class MapInstaller : MonoInstaller
{
    [SerializeField]
    private Game.Map.MapModel _map;

    public override void InstallBindings()
    {
        Container.Bind<Game.Map.Builder>().AsSingle();
        Container.Bind<Highlighter>().AsSingle();
        Container.Bind<PathFinder>().AsSingle();
        Container.Bind<Terraformer>().AsSingle();

        Container.Bind<Game.Map.MapModel>()
            .FromInstance(_map)
            .AsSingle();
    }
}
