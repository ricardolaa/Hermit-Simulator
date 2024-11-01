using System;
using UnityEngine;

public class FireParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private readonly Tuple<float, float> _firePowerRange = new Tuple<float, float>(0, 5);

    public void UpdateParticleLifetime(float percent)
    {
        if (percent < 0 || percent > 100)
            throw new ArgumentOutOfRangeException(nameof(percent));

        var main = _particleSystem.main;

        float newLifetime = Mathf.Lerp(_firePowerRange.Item1, _firePowerRange.Item2, percent / 100f);

        main.startLifetime = newLifetime;
    }
}
