using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public class Fsm
    {
        private FsmState _currentState;
        private readonly Dictionary<Type, FsmState> _states = new Dictionary<Type, FsmState>();

        public FsmState CurrentState => _currentState;

        public void AddState(FsmState state)
        {
            var newState = state.GetType();
            if (_states.ContainsKey(newState))
            {
                Debug.LogWarning($"[State Machine] State {newState.Name} already added!");
                return;
            }
            _states.Add(newState, state);
        }

        public void SetState<T>(object data = null) where T : FsmState
        {
            var newState = typeof(T);

            if (_currentState != null && _currentState.GetType() == newState)
            {
                Debug.Log($"[State Machine] Already in a state {newState.Name}");
                return;
            }

            if (_states.TryGetValue(newState, out var state))
            {
                Debug.Log($"[State Machine] Transition: {_currentState?.GetType().Name ?? "NULL"} > {newState.Name}");

                _currentState?.ExitState();
                _currentState = state;
                _currentState.Init(data);
                _currentState.EnterState();
            }
            else
            {
                Debug.LogError($"[State Machine] State {newState.Name} not found!");
            }
        }

        public bool CheckNewState<T>() where T : FsmState
        {
            var newState = typeof(T);
            if (_currentState.GetType() == newState)
                return false;
            else
                return true;
        }

        public void Update() => _currentState?.UpdateState();
        public void FixedUpdate() => _currentState?.FixedUpdateState();
        public void LateUpdate() => _currentState?.LateUpdateState();
    }
}
