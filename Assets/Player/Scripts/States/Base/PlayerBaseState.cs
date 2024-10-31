using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : State
{
    protected CharacterController _characterController;
    protected PlayerStateData _playerData;
    protected FloorCheck _floorCheck;
    protected Animator _animator;

    protected virtual void InitializeComponents()
    {
        _playerData = AttachedEntity.GetComponent<PlayerStateData>();

        _characterController = AttachedEntity.GetComponent<CharacterController>();
        _floorCheck = AttachedEntity.GetComponent<FloorCheck>();
        _animator = AttachedEntity.GetComponent<Animator>();
    }

}
