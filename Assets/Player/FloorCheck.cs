using UnityEngine;

public class FloorCheck : MonoBehaviour
{
    [SerializeField] private Collider _myCollider;
    [SerializeField] private Vector3 _boxSize = new Vector3(0.5f, 0.1f, 0.5f);
    [SerializeField] private Vector3 _boxOffset = new Vector3(0.5f, -0.1f, 0f);

    private bool isFloor = false;

    void Update()
    {
        CheckForCollidersUnderneath();
    }

    private void CheckForCollidersUnderneath()
    {
        if (_myCollider == null) return;

        Vector3 checkPosition = _myCollider.bounds.center + _boxOffset;

        Collider[] hitColliders = Physics.OverlapBox(checkPosition, _boxSize / 2, Quaternion.identity);

        isFloor = false;
        foreach (var collider in hitColliders)
        {
            if (collider != _myCollider) 
            {
                isFloor = true;
                break;
            }
        }
    }

    public bool IsFloor()
    {
        return isFloor;
    }

    private void OnDrawGizmos()
    {
        if (_myCollider != null)
        {
            Gizmos.color = Color.red;
            Vector3 checkPosition = _myCollider.bounds.center + _boxOffset;
            Gizmos.DrawWireCube(checkPosition, _boxSize);
        }
    }
}
