using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowParticleCollisionEnterCommandData
{
    public ParticleSystem ParticleSystem;
    public GameObject ArrowGameObject;

    public ArrowParticleCollisionEnterCommandData(ParticleSystem particleSystem, GameObject arrowGameObject)
    {
        ParticleSystem = particleSystem;
        ArrowGameObject = arrowGameObject;
    }
}
