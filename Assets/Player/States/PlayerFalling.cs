using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFalling : MoovingState
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
        if(_floorCheck.IsFloor() && _playerMovement.velocity.y < 0)
        {
            _playerMovement.velocity.y = -2f;
        } 
    }

    public override void StateProcess()
    {
        if (_floorCheck.IsFloor())
        {
            TransitionTo(nameof(PlayerIdle));
            return;
        }
        Move();

        Fall();
    }

    private void Fall()
    {
        _playerMovement.velocity.y += _gravity * Time.deltaTime;

        _characterController.Move(_playerMovement.velocity * Time.deltaTime);
    }

}
