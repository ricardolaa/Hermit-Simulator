using System;
using System.Collections;
using UnityEngine;

public class Fire : MonoBehaviour, IHeatEmitter
{
    [SerializeField] private float _heatOutput;
    [SerializeField] private const float thermalDecayConstant = 0.1f;
    [SerializeField] private float _degrees = 900.0f;

    private Coroutine _temperatureCoroutine;

    private SphereCollider _sphereCollider;

    private const float _degreesPerJoule = 0.0005f;

    public float Power => 0.9f * 5.67f * 3.14f * MathF.Pow(10, -8) * MathF.Pow(273.15f + _degrees, 4);

    public float HeatOutput => _heatOutput;
    public float MaxEffectiveDistance => MathF.Sqrt(Power / ((0.1f / _degreesPerJoule) * 4 * MathF.PI));

    private void Awake()
    {
        _sphereCollider = gameObject.AddComponent<SphereCollider>();
        _sphereCollider.radius = MaxEffectiveDistance;
        _sphereCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ITemperatureDependent>(out var temperatureDependent))
        {
            _temperatureCoroutine = StartCoroutine(UpdateTemperature(other, temperatureDependent));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_temperatureCoroutine != null)
        {
            StopCoroutine(_temperatureCoroutine);
            _temperatureCoroutine = null;
        }
    }

    private IEnumerator UpdateTemperature(Collider other, ITemperatureDependent temperatureDependent)
    {
        while (true)
        {
            float distance = Vector3.Distance(transform.position, other.transform.position);
            float temperatureEffect = GetTemperatureEffect(distance);

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

    public float GetTemperatureEffect(float distance)
    {
        if (distance > MaxEffectiveDistance)
            return 0;

        return GetAmountHeat(distance) * _degreesPerJoule;
    }

    public void SetHeatOutput(float newHeatOutput)
    {
        _heatOutput = newHeatOutput;
    }

    public void ToggleHeatEmission(bool isActive)
    {
        throw new NotImplementedException();
    }
}
