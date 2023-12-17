using CoreDomain.GameDomain;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.GameKeyboardInputsModule;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.MainGameUi;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.Score;
using CoreDomain.Scripts.Utils.Command;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class StabbedBullseyeCommand : Command<StabbedBullseyeCommand>
{
    private readonly IMainGameUiModule _mainGameUiModule;
    private readonly IScoreModule _scoreModule;
    private readonly IGameInputActionsModule _gameInputActionsModule;
    private readonly DisposeLevelCommand.Factory _disposeLevelCommand;
    private readonly StartLevelCommand.Factory _startLevelCommand;
    private readonly ILevelsService _levelsService;

    public StabbedBullseyeCommand(IMainGameUiModule mainGameUiModule, IScoreModule scoreModule, IGameInputActionsModule gameInputActionsModule,
        DisposeLevelCommand.Factory disposeLevelCommand, StartLevelCommand.Factory startLevelCommand, ILevelsService levelsService)
    {
        _mainGameUiModule = mainGameUiModule;
        _scoreModule = scoreModule;
        _gameInputActionsModule = gameInputActionsModule;
        _disposeLevelCommand = disposeLevelCommand;
        _startLevelCommand = startLevelCommand;
        _levelsService = levelsService;
    }

    public override async UniTask Execute()
    {
        _mainGameUiModule.ShowWinPanel(_scoreModule.PlayerScore);
        _gameInputActionsModule.DisableInputs();
        
        // if we exceed the number of levels, we stay on the last level
        var nextLevelNumber = Mathf.Min(_levelsService.LastSavedLevelNumber + 1, _levelsService.GetLevelsAmount()); 
        
        _levelsService.SetLastSavedLevel(nextLevelNumber);
        
        await UniTaskHandler.WaitForAnyKeyPressed();
        
        _disposeLevelCommand.Create().Execute();
        _startLevelCommand.Create(new StartLevelCommandData(_levelsService.LastSavedLevelNumber)).Execute().Forget();
    }
}
