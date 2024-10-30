public interface IHeatEmitter
{
    float HeatOutput { get; }
    float MaxEffectiveDistance { get; }

    void SetHeatOutput(float newHeatOutput);

    float GetTemperatureEffect(float distance);

    void ToggleHeatEmission(bool isActive);
}
