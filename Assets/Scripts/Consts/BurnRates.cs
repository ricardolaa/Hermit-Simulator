using System.Collections.Generic;

static public class BurnRates
{
    static public readonly Dictionary<string, float> BurnRatesDict = new Dictionary<string, float>
    {
        { "oak", 0.00030f * 100},
        { "coal", 0.00020f * 100}
    };

    public static float GetBurnRates(string name)
    {
        if (BurnRatesDict.TryGetValue(name.ToLower(), out float temperature))
            return temperature;

        throw new KeyNotFoundException($"Material '{name}' not found in the dictionary.");
    }
}
