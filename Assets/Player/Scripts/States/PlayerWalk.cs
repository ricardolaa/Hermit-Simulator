using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MoovingState
{
    private void Start()
    {
        base.InitializeComponents();
    }

    public override void OnEnter()
    {
        _playerData.velocity = Vector3.zero;
    }

    public override void StateProcess()
    {
        Move();

        if (Input.GetMouseButtonDown(0))
        {
            TransitionTo(nameof(AttackState));
            return;
        }

        if (!_floorCheck.IsFloor())
        {
            _playerData.velocity.y += _gravity * Time.deltaTime;
        }
        else
        {
            _playerData.velocity.y = 0;
        }

        if (!_floorCheck.IsFloor() && _playerData.velocity.y < -2)
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
            return;
        }

    }

    protected override Vector3 HandleMovementInput()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (x == 0 && z == 0)
        {
            TransitionTo(nameof(PlayerIdle));
            return new Vector3(x, 0, z);
        }
        else
        {
            return new Vector3(x, 0, z);
        }
    }   
}   
