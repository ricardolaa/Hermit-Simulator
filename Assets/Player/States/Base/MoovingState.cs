using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoovingState : State
{
    protected CharacterController _characterController;
    protected PlayerStateData _playerMovement;
    protected FloorCheck _floorCheck;

    protected virtual void Move()
    {
        var _body = AttachedEntity.GetComponent<PlayerStateData>().Body;
        var _speed = AttachedEntity.GetComponent<PlayerStateData>().Speed;

        Vector3 direction = _body.transform.TransformDirection(HandleMovementInput());

        _characterController.Move(direction * _speed * Time.deltaTime);
    }

    protected virtual Vector3 HandleMovementInput()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        return new Vector3(x, 0, z);

    }
}
