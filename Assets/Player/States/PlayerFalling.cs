using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFalling : MoovingState
{
    private float _gravity;

    private const float _jumpBufferTime = 0.3f;
    private float _jumpBufferCounter;
    
    private void Start()
    {
        _characterController = AttachedEntity.GetComponent<CharacterController>();
        _playerMovement = AttachedEntity.GetComponent<PlayerStateData>();
        _floorCheck = AttachedEntity.GetComponent<FloorCheck>();

        _gravity = _playerMovement.Gravity;
    }

    public override void OnEnter()
    {
        _playerMovement.velocity.y = -2f;

        _jumpBufferCounter = 0;
    }

    public override void StateProcess()
    {
        if (_floorCheck.IsFloor())
        {
            if (_jumpBufferCounter > 0f)
            {
                TransitionTo(nameof(PlayerJumping));
                return;
            }

            if (Input.GetButton("Jump"))
            {
                TransitionTo(nameof(PlayerJumping));
                return;
            }

            TransitionTo(nameof(PlayerIdle));
            return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            _jumpBufferCounter = _jumpBufferTime;
        }

        if (_jumpBufferCounter > 0f)
        {
            _jumpBufferCounter -= Time.deltaTime;
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
