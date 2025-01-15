using Game.Castle;
using UnityEngine;
using Zenject;

public class CastleInstaller : MonoInstaller
{
    [SerializeField]
    private CastleModel _castleModel;

    [SerializeField]
    private ResourcesModel _resourcesModel;

    public override void InstallBindings()
    {
        Container.Bind<CastleModel>().FromInstance(_castleModel);
        Container.Bind<ResourcesModel>().FromInstance(_resourcesModel);
    }
}
