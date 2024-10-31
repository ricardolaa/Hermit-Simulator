using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerStateData : MonoBehaviour
{
    [SerializeField] private GameObject _body;

    [SerializeField] private float _speed = 12f;
    [SerializeField] private float _jumpHeight = 3f;

    [SerializeField] private CharacterController _characterController;
    [SerializeField] private FloorCheck _floorCheck;
    [SerializeField] private Animator _animator;

    public Vector3 velocity;

    public float Speed => _speed;
    public float Gravity => -Planet.Instance.g * 2;
    public float JumpHeight => _jumpHeight;
    public GameObject Body => _body;

    public CharacterController CharacterController => _characterController;
    public FloorCheck FloorCheck => _floorCheck;
    public Animator Animator => _animator;

}

