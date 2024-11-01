using UnityEngine;

[RequireComponent(typeof(Fire), typeof(FireCollision), typeof(FireLifetime))]
[RequireComponent(typeof(FireParticle))]
public class FirePool : MonoBehaviour
{
    private Fire _fire;
    private FireCollision _collision;
    private FireLifetime _lifetime;
    private FireParticle _particle;

    private void Awake()
    {
        _collision = GetComponent<FireCollision>();
        _lifetime = GetComponent<FireLifetime>();
        _fire = GetComponent<Fire>();
        _particle = GetComponent<FireParticle>();

        _lifetime.OnLifetimePercentChanged += OnLifetimePercentChanged;
        _fire.OnDegressChanged += OnDegressChanged;
    }

    private void Start()
    {
        _collision.SetSphereRadius(_fire.MaxEffectiveDistance);
        _particle.UpdateParticleLifetime(0);
    }

    private void OnLifetimePercentChanged(float percent)
    {
        _fire.SetDegress(_lifetime.Fuel.FlameTemperature * (percent / 100));
        _particle.UpdateParticleLifetime(percent);
    }

    private void OnDegressChanged(float degress)
    {
        _collision.SetSphereRadius(_fire.MaxEffectiveDistance);
    }
}