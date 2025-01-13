using Game.Enemies;
using UnityEngine;
using Zenject;

public class EnemiesInstaller : MonoInstaller
{
    [SerializeField]
    private Spawner _spawner;

    public override void InstallBindings()
    {
        Container.Bind<WaveGenerator>().AsSingle();

        Container.Bind<Spawner>().FromInstance(_spawner);
    }
}