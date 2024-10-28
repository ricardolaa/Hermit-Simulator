using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _distance = 5.0f;
    [SerializeField] private float _height = 2.0f;
    [SerializeField] private float _rotationSpeed = 5.0f;
    [SerializeField] private float _verticalRotationLimit = 80.0f; // Ограничение по вертикали

    private float _currentAngle = 0.0f;
    private float _currentVerticalAngle = 0.0f; // Новый угол для вертикального вращения

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        float horizontalInput = Input.GetAxis("Mouse X");
        float verticalInput = Input.GetAxis("Mouse Y");

        // Поворачиваем игрока только по оси Y
        _currentAngle += horizontalInput * _rotationSpeed;
        _player.rotation = Quaternion.Euler(0, _currentAngle, 0); // Поворачиваем игрока

        // Управляем вертикальным вращением камеры
        _currentVerticalAngle -= verticalInput * _rotationSpeed; // Инвертируем вертикальное вращение

        // Ограничиваем вертикальный угол
        _currentVerticalAngle = Mathf.Clamp(_currentVerticalAngle, -_verticalRotationLimit, _verticalRotationLimit);

        Vector3 offset = new Vector3(0, _height, -_distance);
        Quaternion rotation = Quaternion.Euler(_currentVerticalAngle, _currentAngle, 0); // Используем вертикальный угол
        Vector3 desiredPosition = _player.position + rotation * offset;

        // Позиционируем камеру
        transform.position = desiredPosition;
        transform.LookAt(_player.position + Vector3.up * _height);
    }
}
