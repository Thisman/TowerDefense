using Game.Core;
using Game.Enemies;
using Game.Environment;
using Zenject;

namespace Game.States
{
    public struct EnemyNightStateData { }

    public class EnemyNightState : IState<EnemyNightStateData>
    {
        [Inject]
        private MenuView _menuView;

        [Inject]
        private GameFSM _gameStates;

        [Inject]
        private PlayerFSM _playerStates;

        [Inject]
        private MusicController _musicController;

        [Inject]
        private Spawner _spawner;

        [Inject]
        private SkyBoxController _skyBoxController;

        private EnemyNightStateData _data;

        public void Enter() { }

        public void Enter(EnemyNightStateData data)
        {
            _data = data;

            StartMusic();

            _skyBoxController.SetNightLightning();

            _spawner.OnWaveSpawnEnded += HandleWaveSpawnEnded;
            _spawner.StartSpawnEnemies();

            _menuView.OnSpellBookOpening += HandleSpellBookOpened;
            _menuView.OnStateChanging += HandleNightEnded;

            _menuView.UpdateEnemySpawnInfo(true);
        }

        public void Update() { }

        public void Exit()
        {

            _spawner.StopSpawnEnemies();
            _spawner.OnWaveSpawnEnded -= HandleWaveSpawnEnded;

            _menuView.SwitchGoToNextButtonState(false);
            _menuView.OnSpellBookOpening -= HandleSpellBookOpened;
            _menuView.OnStateChanging -= HandleNightEnded;
            _menuView.UpdateEnemySpawnInfo(false);
        }

        public EnemyNightStateData GetData()
        {
            return _data;
        }

        private void HandleSpellBookOpened()
        {
            _playerStates.SwitchState<SpellBookState, SpellBookStateData>(new SpellBookStateData());
        }

        private void HandleNightEnded()
        {
            _gameStates.SwitchState<RewardsState, RewardsStateData>(new RewardsStateData());
        }

        private void HandleWaveSpawnEnded()
        {
            _menuView.SwitchGoToNextButtonState(true);
        }

        private void StartMusic()
        {
            _musicController.FadeVolume(0, 1, () =>
            {
                _musicController.PlayMusic("Firesong");
                _musicController.FadeVolume(.3f, 1);
            });
        }
    }
}
