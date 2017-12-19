using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://forum.unity.com/threads/wind-that-affects-rigidbodies-and-particles.330944/
// Cant set the following with code so you need to do it manually
// 1 - Enable "External Forces"
// 2 - Disable "Renderer"

[RequireComponent(typeof(ParticleSystem))]
public class AffectedByWind : MonoBehaviour
{
    public Transform obj;
    ParticleSystem particlesSystem;
    ParticleSystem.Particle[] particles;
    Rigidbody body;

    void Start()
    {
        particlesSystem = gameObject.GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[1];
        SetupParticleSystem();
        body = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        particlesSystem.GetParticles(particles);

        body.velocity += particles[0].velocity;
        particles[0].position = body.position;
        particles[0].velocity = Vector3.zero;

        particlesSystem.SetParticles(particles, 1);
    }

    void SetupParticleSystem()
    {
        var ps = particlesSystem.main;
        ps.startLifetime = Mathf.Infinity;
        ps.startSpeed = 0;
        ps.simulationSpace = ParticleSystemSimulationSpace.World;
        ps.maxParticles = 1;

        var emi = particlesSystem.emission;
        emi.rateOverTime = 1;

        particlesSystem.Emit(1);
        particlesSystem.GetParticles(particles);
        particles[0].position = Vector3.zero;
        particlesSystem.SetParticles(particles, 1);
    }
}