using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerStateData : MonoBehaviour
{
    [SerializeField] private GameObject _body;

    [SerializeField] private float _speed = 12f;
    [SerializeField] private float _gravity = -9.81f * 2;
    [SerializeField] private float _jumpHeight = 3f;

    private CharacterController _characterController;

    private bool _isGrounded;
    private bool _isMoving;

    private Vector3 _lastPosition = Vector3.zero;

    public Vector3 velocity;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public float Speed => _speed;
    public float Gravity => _gravity;
    public float JumpHeight => _jumpHeight;
    public GameObject Body => _body;

}

