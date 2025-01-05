using Game.Core;
using Game.Enemies;
using Game.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        private EnemyNightStateData _data;

        public void Enter() { }

        public void Enter(EnemyNightStateData data)
        {
            _data = data;

            StartMusic();

            _spawner.OnFinishWaves += HandleFinishWaves;
            _spawner.StartSpawnEnemies();

            _menuView.OpenSpellBook += HandleOpenSpellBook;
            _menuView.GoToNextState += HandleEndNight;
        }

        public void Update() { }

        public void Exit()
        {

            _spawner.StopSpawnEnemies();
            _spawner.OnFinishWaves -= HandleFinishWaves;

            _menuView.SwitchGoToNextButtonState(false);
            _menuView.OpenSpellBook -= HandleOpenSpellBook;
            _menuView.GoToNextState -= HandleEndNight;
        }

        public EnemyNightStateData GetData()
        {
            return _data;
        }

        private void HandleOpenSpellBook()
        {
            _playerStates.SwitchState<SpellBookState, SpellBookStateData>(new SpellBookStateData());
        }

        private void HandleEndNight()
        {
            _gameStates.SwitchState<RewardsState, RewardsStateData>(new RewardsStateData());
        }

        private void StartMusic()
        {
            _musicController.FadeVolume(0, 1, () =>
            {
                _musicController.PlayMusic("Firesong");
                _musicController.FadeVolume(1, 1);
            });
        }

        private void HandleFinishWaves()
        {
            _menuView.SwitchGoToNextButtonState(true);
        }
    }
}
