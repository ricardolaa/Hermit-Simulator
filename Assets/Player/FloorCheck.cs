using UnityEngine;

public class FloorCheck : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;

    public bool IsFloor()
    {
        return characterController.isGrounded;
    }

}
