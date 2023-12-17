using CoreDomain.Scripts.Utils.Command;
using CoreDomain.Services;
using UnityEngine;

public class ArrowTriggerEnterCommand : CommandSyncOneParameter<ArrowTriggerEnterCommandData, ArrowTriggerEnterCommand>
{
    private readonly IFXModule _fxModule;
    private readonly ScoreChangedCommand.Factory _scoreChangedCommand;
    private readonly IAudioService _audioService;
    private readonly IBalloonsModule _balloonsModule;
    private readonly Collider _collider;

    public ArrowTriggerEnterCommand(ArrowTriggerEnterCommandData commandData, IFXModule fxModule, ScoreChangedCommand.Factory scoreChangedCommand, IAudioService audioService, IBalloonsModule balloonsModule)
    {
        _fxModule = fxModule;
        _scoreChangedCommand = scoreChangedCommand;
        _audioService = audioService;
        _balloonsModule = balloonsModule;
        _collider = commandData.Collider;
    }
    
    public override void Execute()
    {
        var otherPopableView = _collider.GetComponent<PopableView>();

        if (otherPopableView != null)
        {
            otherPopableView.Pop();
        }
    }
}
