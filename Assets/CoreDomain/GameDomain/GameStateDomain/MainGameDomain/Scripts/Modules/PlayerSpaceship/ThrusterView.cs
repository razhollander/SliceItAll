using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterView : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private ThrusterBoostSettings _noBoostSettings;
    [SerializeField] private ThrusterBoostSettings _boostSettings;

    public void EnabledParticles(bool isEnabled)
    {
        if (isEnabled)
        {
            _particleSystem.Play();
        }
        else
        {
            _particleSystem.Stop();
        }
    }

    public void EnableBoost(bool isEnabled)
    {
        ApplyBoostSettings(isEnabled ? _boostSettings : _noBoostSettings);
    }

    private void ApplyBoostSettings(ThrusterBoostSettings boostSettings)
    {
        var particleMain = _particleSystem.main;
        particleMain.startLifetimeMultiplier = boostSettings.ParticleLifeTime;
        
        var colorOverLifetime = _particleSystem.colorOverLifetime;
        colorOverLifetime.color = boostSettings.ColorGradient;
       
        particleMain.startSize = new ParticleSystem.MinMaxCurve(boostSettings.ParticleStartSizeMin,boostSettings.ParticleStartSizeMax);
        //startSize.constantMin = boostSettings.ParticleStartSizeMin;
        //startSize.constantMax = boostSettings.ParticleStartSizeMax;
    }
    
    [System.Serializable]
    public class ThrusterBoostSettings
    {
        public Gradient ColorGradient;
        public float ParticleLifeTime;
        public float ParticleStartSizeMin;
        public float ParticleStartSizeMax;
    }
}
