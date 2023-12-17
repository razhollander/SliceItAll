using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.Score;
using CoreDomain.Scripts.Utils.Command;

public class DisposeLevelCommand : CommandSync<DisposeLevelCommand>
{
    private readonly ILevelTrackModule _levelTrackModule;
    private readonly IScoreService _scoreService;
    private readonly IArrowModule _arrowModule;

    public DisposeLevelCommand(ILevelTrackModule levelTrackModule, IScoreService scoreService, IArrowModule arrowModule)
    {
        _levelTrackModule = levelTrackModule;
        _scoreService = scoreService;
        _arrowModule = arrowModule;
    }
    
    public override void Execute()
    {
        _levelTrackModule.Dispose();
        _scoreService.ResetScore();
        _arrowModule.Dispose();
    }
}
