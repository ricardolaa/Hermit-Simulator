using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumping : MoovingState
{
    private float _gravity;
    private float _jumpHeight;

    private void Start()
    {
        _characterController = AttachedEntity.GetComponent<CharacterController>();
        _playerMovement = AttachedEntity.GetComponent<PlayerStateData>();
        _floorCheck = AttachedEntity.GetComponent<FloorCheck>();

        _gravity = _playerMovement.Gravity;
        _jumpHeight = _playerMovement.JumpHeight;
    }

    public override void OnEnter()
    {
        Jump();
    }

    public override void StateProcess()
    {
        Move();

        _playerMovement.velocity.y += _gravity * Time.deltaTime;

        _characterController.Move(_playerMovement.velocity * Time.deltaTime);
        if (_playerMovement.velocity.y < 0)
        {
            TransitionTo(nameof(PlayerFalling));
        }
    }

    private void Jump()
    {
        _playerMovement.velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
    }
}
