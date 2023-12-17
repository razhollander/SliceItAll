using System.Collections.Generic;
using CoreDomain.Scripts.Utils.Command;
using UnityEngine;

public class ArrowParticleCollisionEnterCommand : CommandSyncOneParameter<ArrowParticleCollisionEnterCommandData, ArrowParticleCollisionEnterCommand>
{
    private readonly ArrowParticleCollisionEnterCommandData _commandData;

    public ArrowParticleCollisionEnterCommand(ArrowParticleCollisionEnterCommandData commandData)
    {
        _commandData = commandData;
    }
    
    public override void Execute()
    {
        var particleSystem = _commandData.ParticleSystem;
        var popableView = particleSystem.GetComponent<PopableView>();

        if (popableView == null) return;

        var collisionEvents = new List<ParticleCollisionEvent>();
        var numCollisionEvents = particleSystem.GetCollisionEvents(_commandData.ArrowGameObject, collisionEvents);

        for (int i = 0; i < numCollisionEvents; i++)
        {
            popableView.Pop(collisionEvents[i].intersection);
        }
    }
}
