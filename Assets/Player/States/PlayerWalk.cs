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

    public override void StateProcess()
    {
        if (!_floorCheck.IsFloor() && _playerMovement.velocity.y < 0) 
        {
            TransitionTo(nameof(PlayerFalling));
            return;
        }
        
        Move();

        _playerMovement.velocity.y += _gravity * Time.deltaTime;
    }


    public override void StateInput(char input)
    {
        if (Input.GetButtonDown("Jump"))
        {
            TransitionTo(nameof(PlayerJumping));
        }
    }

    protected override Vector3 HandleMovementInput()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (x == 0 && z == 0)
        {
            TransitionTo(nameof(PlayerIdle));
            return Vector3.zero;
        }
        else
        {
            return new Vector3(x, 0, z);
        }
    }   
}   
