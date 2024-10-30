using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoovingState : PlayerBaseState
{
    protected float _gravity => _playerData.Gravity;

    protected override void InitializeComponents()
    {
        base.InitializeComponents();
    }

    protected virtual void Move()
    {
        var _body = _playerData.Body;
        var _speed = _playerData.Speed;

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
