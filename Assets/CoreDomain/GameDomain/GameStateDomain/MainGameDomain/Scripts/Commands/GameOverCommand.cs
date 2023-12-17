using CoreDomain.GameDomain;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.GameKeyboardInputsModule;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.MainGameUi;
using CoreDomain.Scripts.Utils.Command;
using Cysharp.Threading.Tasks;

public class GameOverCommand : Command<GameOverCommand>
{
    private readonly IMainGameUiModule _mainGameUiModule;
    private readonly IGameInputActionsModule _gameInputActionsModule;
    private readonly DisposeLevelCommand.Factory _disposeLevelCommand;
    private readonly StartLevelCommand.Factory _startLevelCommand;
    private readonly ILevelsService _levelsService;

    public GameOverCommand(IMainGameUiModule mainGameUiModule,
        IGameInputActionsModule gameInputActionsModule, 
        DisposeLevelCommand.Factory disposeLevelCommand, 
        StartLevelCommand.Factory startLevelCommand, 
        ILevelsService levelsService)
    {
        _mainGameUiModule = mainGameUiModule;
        _gameInputActionsModule = gameInputActionsModule;
        _disposeLevelCommand = disposeLevelCommand;
        _startLevelCommand = startLevelCommand;
        _levelsService = levelsService;
    }

    public override async UniTask Execute()
    {
        _mainGameUiModule.ShowGameOverPanel();
        _gameInputActionsModule.DisableInputs();

        await UniTaskHandler.WaitForAnyKeyPressed();
        
        await UniTask.Yield(); // when we start a level we wait for another 'WaitForAnyKeyPressed'
                               // so needed to add another extra frame here, because sometimes both of 'WaitForAnyKeyPressed'
                               // were invoked on the same frame

        _disposeLevelCommand.Create().Execute();
        _startLevelCommand.Create(new StartLevelCommandData(_levelsService.LastSavedLevelNumber)).Execute().Forget();
    }
}
