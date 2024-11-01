using System;
using System.Collections;
using UnityEngine;

public class FireLifetime : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private float _lifetimePercent = 0;
    [SerializeField, Min(0)] private float _timeNeedToPickFire = 10;
    [SerializeField] private Fuel _fuel;
    [SerializeField] private ParticleSystem _particleSystem;

    private Fire _fire;
    private float _temperature => _fuel.FlameTemperature;

    public float LifetimePercent
    {
        get => _lifetimePercent;
        private set
        {
            _lifetimePercent = Mathf.Clamp(value, 0, 100);
            _fire.SetDegress(_temperature * (_lifetimePercent / 100));
            UpdateParticleLifetime();
        }
    }

    private readonly Tuple<float, float> _firePowerRange = new Tuple<float, float>(0, 5);

    private void OnValidate()
    {
        if (_particleSystem != null && _fire != null)
        {
            UpdateParticleLifetime();
            _fire.SetDegress(_temperature * (_lifetimePercent / 100));
        }
    }

    private void Start()
    {
        if (_particleSystem == null)
            throw new System.ArgumentNullException(nameof(_particleSystem), "Particle system is not assigned.");

        _fire = GetComponent<Fire>();
        UpdateParticleLifetime();
        StartCoroutine(IncreaseLifetimePercent());
        StartCoroutine(BurnFuel());
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

    private IEnumerator BurnFuel()
    {
        var speed = _fuel.BurnRate;

        while (_fuel.Mass > 0) 
        {
            if (speed >= _fuel.Mass)
            {
                _fuel.BurnFuel(_fuel.Mass);
            }
            else
            {
                _fuel.BurnFuel(speed);
                yield return new WaitForSeconds(1);
            }

        }
    }

    private void UpdateParticleLifetime()
    {
        var main = _particleSystem.main;

        float newLifetime = Mathf.Lerp(_firePowerRange.Item1, _firePowerRange.Item2, LifetimePercent / 100f);

        main.startLifetime = newLifetime;
    }
}

