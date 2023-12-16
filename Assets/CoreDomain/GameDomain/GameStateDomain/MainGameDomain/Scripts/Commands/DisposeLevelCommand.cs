using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.Score;
using CoreDomain.Scripts.Utils.Command;
using Cysharp.Threading.Tasks;

public class DisposeLevelCommand : CommandSync<DisposeLevelCommand>
{
    private readonly ILevelTrackModule _levelTrackModule;
    private readonly IScoreModule _scoreModule;
    private readonly IArrowModule _arrowModule;

    public DisposeLevelCommand(ILevelTrackModule levelTrackModule, IScoreModule scoreModule, IArrowModule arrowModule)
    {
        _levelTrackModule = levelTrackModule;
        _scoreModule = scoreModule;
        _arrowModule = arrowModule;
    }
    
    public override void Execute()
    {
        _levelTrackModule.Dispose();
        _scoreModule.ResetScore();
        _arrowModule.Dispose();
    }
}
