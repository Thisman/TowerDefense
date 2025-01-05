
namespace Game.Core
{
    public interface IState
    {
        public void Enter();

        public void Exit();
    
        public void Update();
    }

    public interface IState<TData> : IState
    {
        public void Enter(TData data);

        public TData GetData();
    }
}
