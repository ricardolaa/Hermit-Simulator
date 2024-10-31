using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : PlayerBaseState
{
    private float _speed;

    private void Start()
    {
        base.InitializeComponents();
    }

    public override void StateProcess()
    {
        _speed = 2 * Mathf.Sqrt(Mathf.Pow(Input.GetAxis("Horizontal"), 2) + Mathf.Pow(Input.GetAxis("Vertical"), 2));
        _animator.SetFloat("Speed", _speed);

        if (Input.GetMouseButtonDown(0))
        {
            TransitionTo(nameof(AttackState));
            return;
        }

        if (_speed >= 1)
        {
            TransitionTo(nameof(PlayerWalk));
            return;
        }

        if (!_floorCheck.IsFloor())
        {
            TransitionTo(nameof(PlayerFalling));
            return;
        }

    }

    public override void StateInput(char input)
    {
        if (Input.GetButtonDown("Jump"))
        {
            TransitionTo(nameof(PlayerJumping));
        }
    }

    private bool IsInputDetected()
    {
        return Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
    }
}
