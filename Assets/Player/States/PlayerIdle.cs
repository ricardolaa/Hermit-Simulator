using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : State
{
    private float _gravity;

    private CharacterController _characterController;
    private PlayerStateData _playerMovement;
    private FloorCheck _floorCheck;

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

        if (IsInputDetected())
        {
            TransitionTo(nameof(PlayerWalk));
        }

        _playerMovement.velocity.y += _gravity * Time.deltaTime; 
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
