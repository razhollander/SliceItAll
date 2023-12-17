using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.MainGameUi;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.Score;
using CoreDomain.Scripts.Utils.Command;

public class ScoreChangedCommand : CommandSyncOneParameter<ScoreChangedCommandData, ScoreChangedCommand>
{
    private readonly ScoreChangedCommandData _commandData;
    private readonly IScoreService _scoreService;
    private readonly IMainGameUiModule _mainGameUiModule;

    public ScoreChangedCommand(ScoreChangedCommandData commandData, IScoreService scoreService, IMainGameUiModule mainGameUiModule)
    {
        _commandData = commandData;
        _scoreService = scoreService;
        _mainGameUiModule = mainGameUiModule;
    }
    
    public override void Execute()
    {
        _scoreService.AddScore((int)_commandData.ScoreAdded);
        _mainGameUiModule.UpdateScore(_scoreService.PlayerScore);
    }
}
