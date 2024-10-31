using System;
using System.Collections;
using UnityEngine;

public class FireLifetime : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private float _lifetimePercent = 0;
    [SerializeField, Min(0)] private float _timeNeedToPickFire = 10;
    [SerializeField] private ParticleSystem _particleSystem;

    private Fire _fire;
    private const float _temp = 900;

    public float LifetimePercent
    {
        get => _lifetimePercent;
        private set
        {
            _lifetimePercent = Mathf.Clamp(value, 0, 100);
            _fire.SetDegress(_temp * (_lifetimePercent / 100));
            UpdateParticleLifetime();
        }
    }

    private readonly Tuple<float, float> _firePowerRange = new Tuple<float, float>(0, 5);

    private void OnValidate()
    {
        if (_particleSystem != null && _fire != null)
        {
            _fire.SetDegress(_temp * (_lifetimePercent / 100));
            UpdateParticleLifetime();
        }
    }

    private void Start()
    {
        if (_particleSystem == null)
            throw new System.ArgumentNullException(nameof(_particleSystem), "Particle system is not assigned.");

        _fire = GetComponent<Fire>();
        UpdateParticleLifetime();
        StartCoroutine(IncreaseLifetimePercent());
    }

    private IEnumerator IncreaseLifetimePercent()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _timeNeedToPickFire)
        {
            elapsedTime += Time.deltaTime;
            LifetimePercent = Mathf.Clamp01(elapsedTime / _timeNeedToPickFire) * 100;

            yield return null;
        }

        LifetimePercent = 100;
    }

    private void UpdateParticleLifetime()
    {
        var main = _particleSystem.main;

        float newLifetime = Mathf.Lerp(_firePowerRange.Item1, _firePowerRange.Item2, LifetimePercent / 100f);

        main.startLifetime = newLifetime;
    }
}

