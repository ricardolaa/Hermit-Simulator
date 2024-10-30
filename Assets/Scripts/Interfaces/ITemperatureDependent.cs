public interface ITemperatureDependent
{
    float CurrentTemperature { get; }

    float MinAllowedTemperature { get; }

    float MaxAllowedTemperature { get; }

    void UpdateState();

    void OnTemperatureChange(float newTemperature);

    (float minTemperature, float maxTemperature) GetOptimalTemperatureRange();

    bool IsInOptimalTemperature();
}
