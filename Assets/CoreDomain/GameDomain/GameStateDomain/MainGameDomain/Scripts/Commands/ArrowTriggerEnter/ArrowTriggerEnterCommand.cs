using CoreDomain.Scripts.Utils.Command;
using CoreDomain.Services;
using UnityEngine;

public class ArrowTriggerEnterCommand : CommandSyncOneParameter<ArrowTriggerEnterCommandData, ArrowTriggerEnterCommand>
{
    private readonly IFXModule _fxModule;
    private readonly ScoreChangedCommand.Factory _scoreChangedCommand;
    private readonly IAudioService _audioService;
    private readonly Collider _collider;

    public ArrowTriggerEnterCommand(ArrowTriggerEnterCommandData commandData, IFXModule fxModule, ScoreChangedCommand.Factory scoreChangedCommand, IAudioService audioService)
    {
        _fxModule = fxModule;
        _scoreChangedCommand = scoreChangedCommand;
        _audioService = audioService;
        _collider = commandData.Collider;
    }
    
    public override void Execute()
    {
        var otherPopableView = _collider.GetComponent<PopableView>();

        if (otherPopableView != null)
        {
            otherPopableView.Pop();
            _audioService.PlayAudio(AudioClipName.BalloonPop, AudioChannelType.Fx, AudioPlayType.OneShot);
            _fxModule.ShowScoreGainedFx(otherPopableView.GetCenterPoint(), 1);
            _scoreChangedCommand.Create(new ScoreChangedCommandData(1)).Execute();
        }
    }
}
