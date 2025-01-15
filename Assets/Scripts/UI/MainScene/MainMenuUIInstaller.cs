using Game.UI;
using UnityEngine;
using Zenject;

public class MainMenuUIInstaller : MonoInstaller
{
    [SerializeField]
    private MainMenuView _mainMenuView;

    public override void InstallBindings()
    {
        Container.Bind<MainMenuView>().FromInstance(_mainMenuView);
    }
}
