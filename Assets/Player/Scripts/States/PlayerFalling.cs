using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFalling : MoovingState
{
    private const float _jumpBufferTime = 0.3f;
    private float _jumpBufferCounter;

    private void Start()
    {
        base.InitializeComponents();
    }

    public override void OnEnter()
    {
        _playerData.velocity.y = -2f;

        _jumpBufferCounter = 0;
    }

    public override void OnExit()
    {
        _animator.SetBool("Jump", false);
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
        _playerData.velocity.y += _gravity * Time.deltaTime;

        _characterController.Move(_playerData.velocity * Time.deltaTime);
    }

}
