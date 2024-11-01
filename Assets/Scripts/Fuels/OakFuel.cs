using System;
using UnityEngine;

public class OakFuel : Fuel
{
    private const string _name = "oak";

    public override float FlameTemperature => FlameTemperatures.FlameTemperaturesDict[_name];
    public override float SpecificHeatOfCombustion => SpecificHeatOfCombustions.SpecificHeatOfCombustionsDict[_name];
    public override float BurnRate => BurnRates.BurnRatesDict[_name];

}