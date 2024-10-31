using UnityEngine;

public class OakFuel : ScriptableObject, IFuel
{
    public float FlameTemperature => FlameTemperatures.FlameTemperaturesDict["oak"];
}