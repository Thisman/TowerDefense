using Game.Castle;
using UnityEngine;
using Zenject;

public class CastleInstaller : MonoInstaller
{
    [SerializeField]
    private Game.Castle.ShopModel _castle;

    [SerializeField]
    private Game.Castle.ShopRenderer _shopRenderer;

    public override void InstallBindings()
    {
        Container.Bind<Game.Castle.ShopModel>()
            .FromInstance(_castle)
            .AsSingle();

        Container.Bind<Game.Castle.ShopRenderer>()
            .FromInstance(_shopRenderer)
            .AsSingle();
    }
}
