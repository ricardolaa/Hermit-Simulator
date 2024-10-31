using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour, IHeatEmitter
{
    [SerializeField] private float _degrees = 900.0f;

    private const float _degreesPerJoule = 0.0005f;

    private SphereCollider _sphereCollider;
    private Dictionary<Collider, Coroutine> _temperatureCoroutines = new Dictionary<Collider, Coroutine>();

    public float Power => 0.9f * 5.67f * 3.14f * MathF.Pow(10, -8) * MathF.Pow(273.15f + _degrees, 4);
    public float MaxEffectiveDistance => MathF.Sqrt(Power / ((0.1f / _degreesPerJoule) * 4 * MathF.PI));
    public float Degress => _degrees;

    private void Awake()
    {
        _sphereCollider = gameObject.AddComponent<SphereCollider>();
        _sphereCollider.radius = MaxEffectiveDistance;
        _sphereCollider.isTrigger = true;
    }

    private void OnValidate()
    {
        if (_sphereCollider != null && _degrees > 0)
        {
            _sphereCollider.radius = MaxEffectiveDistance;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ITemperatureDependent>(out var temperatureDependent))
        {
            Coroutine coroutine = StartCoroutine(UpdateTemperature(other, temperatureDependent));
            _temperatureCoroutines[other] = coroutine;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<ITemperatureDependent>(out var temperatureDependent))
        {
            StopCoroutine(_temperatureCoroutines[other]);
            _temperatureCoroutines.Remove(other);
        }
    }

    private IEnumerator UpdateTemperature(Collider other, ITemperatureDependent temperatureDependent)
    {
        while (true)
        {
            float distance = Vector3.Distance(transform.position, other.transform.position);
            float temperatureEffect = GetTemperatureEffect(distance, temperatureDependent);

            if (distance < MaxEffectiveDistance / 10)
            {
                if (temperatureDependent.CurrentTemperature + temperatureEffect > temperatureDependent.MaxAllowedTemperature * (distance * distance) * 100)
                {
                    yield return new WaitForSeconds(1f);
                }
                else
                {
                    temperatureDependent.OnTemperatureChange(temperatureEffect);
                }
            }
            else
            {
                if (temperatureDependent.CurrentTemperature + distance > temperatureDependent.MaxAllowedTemperature)
                {
                    yield return new WaitForSeconds(1f);
                }
                else
                {
                    temperatureDependent.OnTemperatureChange(temperatureEffect);
                }
            }

            yield return new WaitForSeconds(1f);
        }
    }

    public float GetAmountHeat(float distance)
    {
        return Power / (4 * MathF.PI * distance * distance);
    }

    public float GetTemperatureEffect(float distance, ITemperatureDependent temperatureDependent)
    {
        if (distance > MaxEffectiveDistance)
            return 0;

        return GetAmountHeat(distance) * _degreesPerJoule;
    }

    public void SetDegress(float newDegress)
    {
        if (newDegress < 0)
            return;

        _degrees = newDegress;
    }
}
