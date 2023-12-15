using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblesView : MonoBehaviour
{
    [SerializeField] private ParticleSystem _bubblesParticleSystem;

    // these lists are used to contain the particles which match
    // the trigger conditions each frame.
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

    void OnParticleTrigger()
    {
        // get the particles which matched the trigger conditions this frame
        int numEnter = _bubblesParticleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

        // iterate through the particles which entered the trigger and make them red
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];
            //p.startColor = new Color32(255, 0, 0, 255);
            //p.position
            enter[i] = p;
        }

        // re-assign the modified particles back into the particle system
        _bubblesParticleSystem.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    }
}
