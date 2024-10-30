using System;
using UnityEngine;

public class PlayerTemperature : MonoBehaviour, ITemperatureDependent
{
    [SerializeField] private float _currentTemperature = 0f;
    [SerializeField] private float _minAllowedTemperature = 0f;
    [SerializeField] private float _maxAllowedTemperature = 0f;

    public float CurrentTemperature => _currentTemperature;

    public float MinAllowedTemperature => _minAllowedTemperature;

    public float MaxAllowedTemperature => _maxAllowedTemperature;

    public (float minTemperature, float maxTemperature) GetOptimalTemperatureRange()
    {
        return (MinAllowedTemperature, MaxAllowedTemperature);
    }

    public bool IsInOptimalTemperature()
    {
        var (minTemperature, maxTemperature) = GetOptimalTemperatureRange();

        return _currentTemperature >= minTemperature && _currentTemperature <= maxTemperature;
    }


    public void OnTemperatureChange(float newTemperature)
    {
        if (newTemperature == 0)
            return;

        _currentTemperature = _currentTemperature + newTemperature;

        if (!IsInOptimalTemperature())
        {
            var health = GetComponent<PlayerHealth>();

            var (minTemperature, maxTemperature) = GetOptimalTemperatureRange();

            float damage = CalculateDamage(minTemperature, maxTemperature);

            health.TakeDamage(damage);
        }
    }

    private float CalculateDamage(float minTemperature, float maxTemperature)
    {
        if (_currentTemperature < minTemperature)
        {
            return CalculateUnderMinDamage(minTemperature);
        }
        else if (_currentTemperature > maxTemperature)
        {
            return CalculateOverMaxDamage(maxTemperature);
        }

        return 0;
    }

    private float CalculateUnderMinDamage(float minTemperature)
    {
        float k = minTemperature / 4;
        float a = GetDamageFactor();

        float damage = -((Mathf.Sqrt(k * _currentTemperature)) / (a * k) + Mathf.Pow(2, 1 - Mathf.Log(a, 2)));
        return Mathf.Max(damage, 0);
    }

    private float CalculateOverMaxDamage(float maxTemperature)
    {
        float k = maxTemperature / 4;
        float a = GetDamageFactor();

        float damage = (Mathf.Sqrt(k * _currentTemperature)) / (a * k) - Mathf.Pow(2, 1 - Mathf.Log(a, 2));
        return Mathf.Max(damage, 0);
    }

    private float GetDamageFactor()
    {
        float a = 0.5f; // The damage increase depends on this value (a > 0)
        if (a <= 0) throw new ArgumentOutOfRangeException(nameof(a));
        return a;
    }


    public void UpdateState()
    {
        throw new System.NotImplementedException();
    }
}
