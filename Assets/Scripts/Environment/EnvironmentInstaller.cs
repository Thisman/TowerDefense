using Game.Environment;
using UnityEngine;
using Zenject;

public class EnvironmentInstaller : MonoInstaller
{
    [SerializeField]
    private SkyBoxController _skyBoxController;

    public override void InstallBindings()
    {
        Container.Bind<SkyBoxController>().FromInstance(_skyBoxController);
    }
}