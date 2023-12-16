using System.Collections;
using System.Collections.Generic;
using CoreDomain.Scripts.Utils.Command;
using CoreDomain.Services;
using UnityEngine;

public class ArrowParticleCollisionEnterCommand : CommandSyncOneParameter<ArrowParticleCollisionEnterCommandData, ArrowParticleCollisionEnterCommand>
{
    private readonly IFXModule _fxModule;
    private readonly ScoreChangedCommand.Factory _scoreChangedCommand;
    private readonly IAudioService _audioService;
    private readonly ArrowParticleCollisionEnterCommandData _commandData;

    public ArrowParticleCollisionEnterCommand(ArrowParticleCollisionEnterCommandData commandData, IFXModule fxModule, ScoreChangedCommand.Factory scoreChangedCommand, IAudioService audioService)
    {
        _fxModule = fxModule;
        _scoreChangedCommand = scoreChangedCommand;
        _audioService = audioService;
        _commandData = commandData;
    }
    
    public override void Execute()
    {
        var particleSystem = _commandData.ParticleSystem;
        var collisionEvents = new List<ParticleCollisionEvent>();
        var numCollisionEvents = particleSystem.GetCollisionEvents(_commandData.ArrowGameObject, collisionEvents);

        for (int i = 0; i < numCollisionEvents; i++)
        {
            _audioService.PlayAudio(AudioClipName.BubblePop, AudioChannelType.Fx, AudioPlayType.OneShot);
            _fxModule.ShowScoreGainedFx(collisionEvents[i].intersection, 2);
            _scoreChangedCommand.Create(new ScoreChangedCommandData(2)).Execute();
        }
    }
}
