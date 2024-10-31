using System.Collections.Generic;

static public class FlameTemperatures
{
    public static readonly Dictionary<string, float> FlameTemperaturesDict = new Dictionary<string, float>
    {
        { "oak", 900},
    };

    public static float GetFlameTemperature(string name)
    {
        if (FlameTemperaturesDict.TryGetValue(name.ToLower(), out float temperature))
            return temperature;

        throw new KeyNotFoundException($"Material '{name}' not found in the dictionary.");
    }

}