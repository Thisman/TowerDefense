using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace Game.Core
{
    public class FSM
    {
        public Action<IState> OnStateChanged;

        private IState currentState;
        private readonly Dictionary<Type, object> states = new Dictionary<Type, object>();

        public void AddState<TState, TData>(IState<TData> state) where TState : IState<TData>
        {
            states[typeof(TState)] = state; // Сохраняем состояние как object
        }

        public void SwitchState<TState, TData>(TData data) where TState : IState<TData>
        {
            Debug.Log("Exit from " + currentState?.GetType());
            currentState?.Exit();

            if (states.TryGetValue(typeof(TState), out var stateObject))
            {
                Debug.Log("Enter in " + stateObject.GetType());
                var newState = (IState<TData>)stateObject;
                currentState = newState;
                newState.Enter(data);

                OnStateChanged?.Invoke(currentState);
            }
            else
            {
                throw new InvalidOperationException($"Состояние {typeof(TState)} не найдено в машине состояний.");
            }
        }

        // Обновление текущего состояния
        public void Update()
        {
            currentState?.Update();
        }
    }
}
