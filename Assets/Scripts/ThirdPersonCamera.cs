using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _distance = 5.0f;
    [SerializeField] private float _height = 2.0f;
    [SerializeField] private float _rotationSpeed = 5.0f;

    private float _currentAngle = 0.0f;
   
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    void LateUpdate()
    {
        float horizontalInput = Input.GetAxis("Mouse X");

        _currentAngle += horizontalInput * _rotationSpeed;

        Vector3 offset = new Vector3(0, _height, -_distance);
        Quaternion rotation = Quaternion.Euler(0, _currentAngle, 0);
        Vector3 desiredPosition = _player.position + rotation * offset;

        _player.rotation = rotation;

        transform.position = desiredPosition;
        transform.LookAt(_player.position + Vector3.up * _height);
    }
}

