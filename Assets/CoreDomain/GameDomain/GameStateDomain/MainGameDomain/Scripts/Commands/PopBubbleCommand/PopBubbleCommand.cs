using CoreDomain.Scripts.Utils.Command;
using CoreDomain.Services;

public class PopBubbleCommand : CommandSyncOneParameter<PopBubbleCommandData, PopBubbleCommand>
{
    private readonly IFXModule _fxModule;
    private readonly ScoreChangedCommand.Factory _scoreChangedCommand;
    private readonly IAudioService _audioService;
    private readonly IBubblesModule _bubblesModule;
    private readonly PopBubbleCommandData _commandData;

    public PopBubbleCommand(PopBubbleCommandData commandData, IFXModule fxModule, ScoreChangedCommand.Factory scoreChangedCommand, IAudioService audioService, IBubblesModule bubblesModule)
    {
        _fxModule = fxModule;
        _scoreChangedCommand = scoreChangedCommand;
        _audioService = audioService;
        _bubblesModule = bubblesModule;
        _commandData = commandData;
    }

    public override void Execute()
    {
        // NOTE: No need to invoke intentionally the bubble pop effect because its being handled by the particle system itself
        
        var popScore = _bubblesModule.BubblesPopScore;
        _fxModule.ShowScoreGainedFx(_commandData.Position, popScore);
        _scoreChangedCommand.Create(new ScoreChangedCommandData(popScore)).Execute();
        _audioService.PlayAudio(AudioClipName.BubblePop, AudioChannelType.Fx, AudioPlayType.OneShot);
    }
}
