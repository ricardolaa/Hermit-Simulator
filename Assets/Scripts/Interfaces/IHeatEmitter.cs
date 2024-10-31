public interface IHeatEmitter
{
    float Degress { get; }
    float MaxEffectiveDistance { get; }

    void SetDegress(float newTemperature);

    float GetTemperatureEffect(float distance, ITemperatureDependent temperatureDependent);
}
