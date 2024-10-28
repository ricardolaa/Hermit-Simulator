using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public GameObject AttachedEntity { get; set; }

    public virtual void OnEnter() { }
    public virtual void OnExit() { }
    public virtual void StateProcess() { }
    public virtual void StateInput(char input) { }

    public delegate void StateTransitionHandler(State sourceState, string newStateName);
    public event StateTransitionHandler StateTransition;

    protected void TransitionTo(string newStateName)
    {
        StateTransition?.Invoke(this, newStateName);
    }

}
