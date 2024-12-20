using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public static Planet Instance { get; private set; }

    [SerializeField, Min(0.1f)] private float _massWithout20Zeros;
    [SerializeField, Min(0.1f)] private float _radiusInKm;

    public float MassInKG => _massWithout20Zeros * MathF.Pow(10, 20);
    public float RadiusInMetr => _radiusInKm * 1000;
    public float g => (6.7f * MathF.Pow(10, -11) * MassInKG) / MathF.Pow(RadiusInMetr, 2);

    private void Awake()
    {
        Instance = this;
    }

}
