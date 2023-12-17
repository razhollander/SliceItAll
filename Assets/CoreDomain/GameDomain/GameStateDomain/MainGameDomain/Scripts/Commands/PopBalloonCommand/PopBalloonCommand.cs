using CoreDomain.Scripts.Utils.Command;
using CoreDomain.Services;

public class PopBalloonCommand : CommandSyncOneParameter<PopBalloonCommandData, PopBalloonCommand>
{
    private readonly PopBalloonCommandData _commandData;
    private readonly IBalloonsModule _balloonsModule;
    private readonly IFXModule _fxModule;
    private readonly ScoreChangedCommand.Factory _scoreChangedCommand;
    private readonly IAudioService _audioService;

    public PopBalloonCommand(PopBalloonCommandData commandData, IBalloonsModule balloonsModule, IFXModule fxModule, ScoreChangedCommand.Factory scoreChangedCommand,
        IAudioService audioService)
    {
        _commandData = commandData;
        _balloonsModule = balloonsModule;
        _fxModule = fxModule;
        _scoreChangedCommand = scoreChangedCommand;
        _audioService = audioService;
    }
    
    public override void Execute()
    {
        var popScore = _balloonsModule.BalloonPopScore;
        _commandData.BalloonView.PlayPopEffect();
        _fxModule.ShowScoreGainedFx(_commandData.Position, popScore);
        _scoreChangedCommand.Create(new ScoreChangedCommandData(popScore)).Execute();
        _audioService.PlayAudio(AudioClipName.BalloonPop, AudioChannelType.Fx, AudioPlayType.OneShot);
    }
}
