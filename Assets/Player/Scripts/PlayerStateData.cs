using UnityEngine;

public class PlayerStateData : MonoBehaviour
{
    [SerializeField] private GameObject _body;

    [SerializeField] private float _speed = 12f;
    [SerializeField] private float _jumpHeight = 3f;

    public Vector3 velocity;

    public float Speed => _speed;
    public float JumpHeight => _jumpHeight;
    public GameObject Body => _body;

}

