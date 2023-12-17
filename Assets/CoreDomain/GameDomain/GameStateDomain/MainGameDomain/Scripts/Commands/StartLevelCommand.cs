using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.MainGameUi;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.Score;
using CoreDomain.Scripts.Utils.Command;
using Cysharp.Threading.Tasks;
using CoreDomain.GameDomain;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.GameKeyboardInputsModule;
using CoreDomain.Services;

public class StartLevelCommand : CommandOneParameter<StartLevelCommandData, StartLevelCommand>
{
    private readonly StartLevelCommandData _commandData;
    private readonly IScoreModule _scoreModule;
    private readonly IMainGameUiModule _mainGameUiModule;
    private readonly ILevelTrackModule _levelTrackModule;
    private readonly ILevelsService _levelsService;
    private readonly IArrowModule _arrowModule;
    private readonly ICameraService _cameraService;
    private readonly IGameInputActionsModule _gameInputActionsModule;
    private readonly IBalloonsModule _balloonsModule;
    private readonly IBubblesModule _bubblesModule;

    public StartLevelCommand(
        StartLevelCommandData commandData,
        IScoreModule scoreModule,
        IMainGameUiModule mainGameUiModule,
        ILevelTrackModule levelTrackModule,
        ILevelsService levelsService,
        IArrowModule arrowModule,
        ICameraService cameraService,
        IGameInputActionsModule gameInputActionsModule,
        IBalloonsModule balloonsModule,
        IBubblesModule bubblesModule)
    {
        _commandData = commandData;
        _scoreModule = scoreModule;
        _mainGameUiModule = mainGameUiModule;
        _levelTrackModule = levelTrackModule;
        _levelsService = levelsService;
        _arrowModule = arrowModule;
        _cameraService = cameraService;
        _gameInputActionsModule = gameInputActionsModule;
        _balloonsModule = balloonsModule;
        _bubblesModule = bubblesModule;
    }

    public override async UniTask Execute()
    {
        _mainGameUiModule.SwitchToBeforeGameView(_levelsService.LastSavedLevelNumber);
        _levelTrackModule.CreateLevelTrack(_levelsService.GetLevelData(_commandData.LevelNumber).LevelTack);
        _arrowModule.CreateArrow();
        _arrowModule.RegisterListeners();
        _cameraService.SetCameraFollowTarget(GameCameraType.World, _arrowModule.ArrowTransform);
        _gameInputActionsModule.EnableInputs();
        _balloonsModule.SetupBalloons();
        _bubblesModule.SetupBubbles();
        
        await UniTaskHandler.WaitForAnyKeyPressed();
        
        _mainGameUiModule.SwitchToInGameView(_scoreModule.PlayerScore);
    }
}
