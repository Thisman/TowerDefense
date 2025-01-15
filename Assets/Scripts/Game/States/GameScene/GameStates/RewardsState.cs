using Game.Core;
using Game.UI;
using Zenject;

namespace Game.States
{
    public struct RewardsStateData { }

    public class RewardsState : IState<RewardsStateData>
    {
        [Inject]
        private GameFSM _gameStates;

        [Inject]
        private PlayerFSM _playerStates;

        [Inject]
        private RewardView _rewardView;

        [Inject]
        private MusicController _musicController;

        private RewardsStateData _data;

        public void Enter() { }

        public void Enter(RewardsStateData data)
        {
            _data = data;

            StartMusic();

            _rewardView.Show();
            _rewardView.OnRewardChosen += HandleRewardChosen;
        }

        public void Update() { }

        public void Exit() {
            _rewardView.Hide();
            _rewardView.OnRewardChosen -= HandleRewardChosen;
        }

        public RewardsStateData GetData()
        {
            return _data;
        }

        private void HandleRewardChosen()
        {
            _gameStates.SwitchState<DayState, DayStateData>(new DayStateData());
            _playerStates.SwitchState<IdleState, IdleStateData>(new IdleStateData());
        }

        private void StartMusic()
        {
            _musicController.FadeVolume(0, 1, () =>
            {
                _musicController.PlayMusic("Heroic Age");
                _musicController.FadeVolume(.3f, 1);
            });
        }
    }
}
