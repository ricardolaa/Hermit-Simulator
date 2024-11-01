using System;
using UnityEngine;

public class FireCollision : MonoBehaviour
{
    private SphereCollider _sphereCollider;

    private void Awake()
    {
        _sphereCollider = gameObject.AddComponent<SphereCollider>();
        _sphereCollider.isTrigger = true;
    }

    public void SetSphereRadius(float radius)
    {
        if (radius < 0)
            throw new ArgumentOutOfRangeException(nameof(radius));

        _sphereCollider.radius = radius;
    }
}