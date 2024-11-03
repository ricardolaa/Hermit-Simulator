using System;
using System.Collections.Generic;
static public class SpecificHeatOfCombustions
{
    public static readonly Dictionary<string, float> SpecificHeatOfCombustionsDict = new Dictionary<string, float>
    {
        { "oak", 10.2f * MathF.Pow(10, 6)},
        { "coal", 29.3f * MathF.Pow(10, 6)}
    };

    public static float GetSpecificHeatOfCombustions(string name)
    {
        if (SpecificHeatOfCombustionsDict.TryGetValue(name.ToLower(), out float temperature))
            return temperature;

        throw new KeyNotFoundException($"Material '{name}' not found in the dictionary.");
    }
}