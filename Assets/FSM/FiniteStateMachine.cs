using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine : MonoBehaviour
{
    [SerializeField] private State _initialState;
    [SerializeField] private GameObject _attachedEntity;

    private Dictionary<string, State> _states = new Dictionary<string, State>();
    private State _currentState;

    void Awake()
    {
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<State>(out State state))
            {
                _states[state.GetType().Name.ToLower()] = state;

                state.StateTransition += (State sourceState, string name) => SwitchStates(sourceState, name);
                state.AttachedEntity = _attachedEntity;
            }
            else
            {
                Debug.LogWarning($"Child {child.name} is not a State for FiniteStateMachine");
            }
        }

        if (_initialState != null)
        {
            _initialState.OnEnter();
            _currentState = _initialState;
        }
    }

    void FixedUpdate()
    {
        _currentState?.StateProcess();
    }

    public void ForceSwitchStates(string newStateName)
    {
        if (_states.TryGetValue(newStateName.ToLower(), out State newState))
        {
            _currentState?.OnExit();
            newState.OnEnter();
            _currentState = newState;
        }
    }

    private void SwitchStates(State sourceState, string newStateName)
    {
        if (sourceState == _currentState && _states.TryGetValue(newStateName.ToLower(), out State newState))
        {
            _currentState?.OnExit();
            newState.OnEnter();
            _currentState = newState;
        }
    }

    void Update()
    {
        if (_currentState != null)
        {
            foreach (var input in Input.inputString)
            {
                _currentState.StateInput(input);
            }
        }
    }
}

