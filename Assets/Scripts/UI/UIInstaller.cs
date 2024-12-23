using Game.UI;
using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField]
    private Game.UI.ActionsRenderer _actionsRenderer;

    [SerializeField]
    private Game.UI.BuildingRenderer _buildingInfoRenderer;

    public override void InstallBindings()
    {
        Container.Bind<ActionsRenderer>()
            .FromInstance(_actionsRenderer)
            .AsSingle();

        Container.Bind<BuildingRenderer>()
            .FromInstance(_buildingInfoRenderer)
            .AsSingle();
    }
}
