using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GlobalHeat : MonoBehaviour, IHeatEmitter
{
    [SerializeField] private float _globalTemperature = 25;
    [SerializeField] private float _heatOutput;

    public float HeatOutput => _heatOutput;

    public float MaxEffectiveDistance => float.PositiveInfinity;

    private List<ITemperatureDependent> _temperatureDependents;

    private void Awake()
    {
        _temperatureDependents = new List<ITemperatureDependent>(FindObjectsOfType<MonoBehaviour>().OfType<ITemperatureDependent>());
    }

    private void Start()
    {
        foreach (var temperatureDependent in _temperatureDependents)
        {
            StartCoroutine(UpdateTemperature(temperatureDependent));
        }
    }

    private IEnumerator UpdateTemperature(ITemperatureDependent temperatureDependent)
    {
        float k = 0.1f; // Температурная константа
        if (k <= 0) throw new ArgumentOutOfRangeException(nameof(k));

        const float tolerance = 0.01f;

        while (true)
        {
            if (MathF.Abs(temperatureDependent.CurrentTemperature - _globalTemperature) < tolerance)
            {
                yield return null;
            }
            else
            {
                var T = _globalTemperature + (temperatureDependent.CurrentTemperature - _globalTemperature) * MathF.Pow(MathF.E, -k);
                var dt = T - temperatureDependent.CurrentTemperature;
                temperatureDependent.OnTemperatureChange(dt);
                yield return new WaitForSeconds(1);
            }
        }
    }


    public float GetTemperatureEffect(float distance) //distance can be ignored
    {
        return HeatOutput;
    }

    public void SetHeatOutput(float newHeatOutput)
    {
        throw new System.NotImplementedException();
    }

    public void ToggleHeatEmission(bool isActive)
    {
        throw new System.NotImplementedException();
    }
}
