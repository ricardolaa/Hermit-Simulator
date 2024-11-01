using System;
using UnityEngine;

public abstract class Fuel : MonoBehaviour
{
    [SerializeField] protected float _mass;

    public abstract float FlameTemperature { get; }
    public abstract float SpecificHeatOfCombustion { get; }
    public abstract float BurnRate { get; }
    public virtual float Mass
    {
        get => _mass;
        protected set => _mass = MathF.Max(0, value);
    }

    public virtual float BurnFuel(float amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        if (amount > Mass)
            return float.NaN;

        Mass -= amount;
        return amount * SpecificHeatOfCombustion;
    }
}
