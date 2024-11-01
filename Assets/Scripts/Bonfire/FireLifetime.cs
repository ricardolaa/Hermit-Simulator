using System;
using System.Collections;
using UnityEngine;

public class FireLifetime : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private float _lifetimePercent = 0;
    [SerializeField] private Fuel _fuel;

    private float _timeNeedToPickFire => 1 / _fuel.BurnRate;

    public event Action<float> OnLifetimePercentChanged;

    public float LifetimePercent
    {
        get => _lifetimePercent;
        private set
        {
            _lifetimePercent = Mathf.Clamp(value, 0, 100);
            OnLifetimePercentChanged?.Invoke(_lifetimePercent);
        }
    }

    public Fuel Fuel => _fuel;

    private void OnValidate()
    {
        if (_fuel != null)
        {
            OnLifetimePercentChanged?.Invoke(_lifetimePercent);
        }
    }

    private void Start()
    {
        StartCoroutine(IncreaseLifetimePercent());
        StartCoroutine(BurnFuel());
    }

    private IEnumerator IncreaseLifetimePercent()
    {
        float elapsedTime = 0f;

        while (_fuel.Mass > 0)
        {
            elapsedTime += Time.deltaTime;
            LifetimePercent = Mathf.Clamp01(elapsedTime / _timeNeedToPickFire) * 100;

            yield return null;
        }

        float burnSpeed = 5;

        while (LifetimePercent > 0)
        {
            LifetimePercent -= Time.deltaTime * burnSpeed;
            LifetimePercent = Mathf.Clamp(LifetimePercent, 0, 100);

            yield return null;
        }
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

}

