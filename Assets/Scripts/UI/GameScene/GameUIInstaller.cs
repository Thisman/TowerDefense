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
    private TowerView _towerView;

    [SerializeField]
    private SpellBookView _spellBookView;

    public override void InstallBindings()
    {
        Container.Bind<MenuView>().FromInstance(_menuView);
        Container.Bind<EnemyView>().FromInstance(_enemyView);
        Container.Bind<RewardView>().FromInstance(_rewardView);
        Container.Bind<CastleView>().FromInstance(_castleView);
        Container.Bind<TowerView>().FromInstance(_towerView);
        Container.Bind<SpellBookView>().FromInstance(_spellBookView);
    }
}
