using System.Collections;
using System.Collections.Generic;
using CoreDomain.Scripts.Utils.Command;
using UnityEngine;

public class ArrowParticleCollisionEnterCommand : CommandSyncOneParameter<ArrowParticleCollisionEnterCommandData, ArrowParticleCollisionEnterCommand>
{
    private readonly IFXModule _fxModule;
    private readonly ScoreChangedCommand.Factory _scoreChangedCommand;
    private readonly ArrowParticleCollisionEnterCommandData _commandData;

    public ArrowParticleCollisionEnterCommand(ArrowParticleCollisionEnterCommandData commandData, IFXModule fxModule, ScoreChangedCommand.Factory scoreChangedCommand)
    {
        _fxModule = fxModule;
        _scoreChangedCommand = scoreChangedCommand;
        _commandData = commandData;
    }
    
    public override void Execute()
    {
        var particleSystem = _commandData.ParticleSystem;
        var collisionEvents = new List<ParticleCollisionEvent>();
        var numCollisionEvents = particleSystem.GetCollisionEvents(_commandData.ArrowGameObject, collisionEvents);

        for (int i = 0; i < numCollisionEvents; i++)
        {
            _fxModule.ShowScoreGainedFx(collisionEvents[i].intersection, 2);
            _scoreChangedCommand.Create(new ScoreChangedCommandData(2)).Execute();
        }
    }
}
