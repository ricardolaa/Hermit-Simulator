using System;
using System.Collections;
using UnityEngine;

public class Fire : MonoBehaviour, IHeatEmitter
{
    [SerializeField] private float _heatOutput;
    [SerializeField] private float _maxEffectiveDistance;
    [SerializeField] private const float thermalDecayConstant = 0.1f;

    private Coroutine _temperatureCoroutine;

    private SphereCollider _sphereCollider;

    public float HeatOutput => _heatOutput;
    public float MaxEffectiveDistance => _maxEffectiveDistance;

    private void Awake()
    {
        _sphereCollider = gameObject.AddComponent<SphereCollider>();
        _sphereCollider.radius = _maxEffectiveDistance;
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

            if (distance < _maxEffectiveDistance / 10)
            {
                if (temperatureDependent.CurrentTemperature + temperatureEffect > temperatureDependent.MaxAllowedTemperature * (distance * distance) * 100)
                {
                    yield return null;
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
                    yield return null;
                }
                else
                {
                    temperatureDependent.OnTemperatureChange(temperatureEffect);
                }
            }

            yield return new WaitForSeconds(1f);
        }
    }



    public float GetTemperatureEffect(float distance)
    {
        if (distance > _maxEffectiveDistance)
            return 0;

        return _heatOutput / (1 + thermalDecayConstant * distance);
    }

    public void SetHeatOutput(float newHeatOutput)
    {
        if (newHeatOutput < 0)
            throw new ArgumentOutOfRangeException(nameof(newHeatOutput));

        _heatOutput = newHeatOutput;
    }

    public void ToggleHeatEmission(bool isActive)
    {
        throw new NotImplementedException();
    }
}
