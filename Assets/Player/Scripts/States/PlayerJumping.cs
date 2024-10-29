using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumping : MoovingState
{
    private float _jumpHeight;

    private void Start()
    {
        base.InitializeComponents();
        _jumpHeight = _playerData.JumpHeight;
    }

    public override void OnEnter()
    {
        _animator.SetBool("Jump", true);
        Jump();
    }

    public override void StateProcess()
    {
        Move();

        _playerData.velocity.y += _gravity * Time.deltaTime;

        _characterController.Move(_playerData.velocity * Time.deltaTime);
        if (_playerData.velocity.y < 0)
        {
            TransitionTo(nameof(PlayerFalling));
        }
    }

    private void Jump()
    {
        _playerData.velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
    }
}
