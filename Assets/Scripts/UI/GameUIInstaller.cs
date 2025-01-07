using UnityEngine;
using Zenject;
using Game.UI;

public class GameUIInstaller : MonoInstaller
{
    [SerializeField]
    private MenuView _menuView;

    [SerializeField]
    private EnemyView _enemyView;

    [SerializeField]
    private RewardView _rewardView;

    [SerializeField]
    private CastleView _castleView;

    [SerializeField]
    private BuildingView _buildingView;

    [SerializeField]
    private SpellBookView _spellBookView;

    public override void InstallBindings()
    {
        Container.Bind<MenuView>().FromInstance(_menuView);
        Container.Bind<EnemyView>().FromInstance(_enemyView);
        Container.Bind<RewardView>().FromInstance(_rewardView);
        Container.Bind<CastleView>().FromInstance(_castleView);
        Container.Bind<BuildingView>().FromInstance(_buildingView);
        Container.Bind<SpellBookView>().FromInstance(_spellBookView);
    }
}
