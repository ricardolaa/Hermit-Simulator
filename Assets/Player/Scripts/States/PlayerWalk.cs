using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MoovingState
{
    private float _gravity;

    private void Start()
    {
        _characterController = AttachedEntity.GetComponent<CharacterController>();
        _playerMovement = AttachedEntity.GetComponent<PlayerStateData>();
        _floorCheck = AttachedEntity.GetComponent<FloorCheck>();

        _gravity = _playerMovement.Gravity;
    }

    public override void OnEnter()
    {
        _playerMovement.velocity = Vector3.zero;
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
            _playerMovement.velocity.y += _gravity * Time.deltaTime;
        }
        else
        {
            _playerMovement.velocity.y += _gravity * -Time.deltaTime;
        }

        if (!_floorCheck.IsFloor() && _playerMovement.velocity.y < -2)
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
