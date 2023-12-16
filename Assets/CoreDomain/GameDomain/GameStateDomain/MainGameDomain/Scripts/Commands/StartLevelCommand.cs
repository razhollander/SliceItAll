using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.MainGameUi;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.Score;
using CoreDomain.Scripts.Utils.Command;
using Cysharp.Threading.Tasks;
using CoreDomain.GameDomain;
using UniRx;
using UnityEngine;

public class StartLevelCommand : CommandOneParameter<StartLevelCommandData, StartLevelCommand>
{
    private readonly StartLevelCommandData _commandData;
    private readonly IScoreModule _scoreModule;
    private readonly IMainGameUiModule _mainGameUiModule;
    private readonly ILevelTrackModule _levelTrackModule;
    private readonly ILevelsService _levelsService;

    public StartLevelCommand(
        StartLevelCommandData commandData,
        IScoreModule scoreModule,
        IMainGameUiModule mainGameUiModule,
        ILevelTrackModule levelTrackModule,
        ILevelsService levelsService)
    {
        _commandData = commandData;
        _scoreModule = scoreModule;
        _mainGameUiModule = mainGameUiModule;
        _levelTrackModule = levelTrackModule;
        _levelsService = levelsService;
    }

    public override async UniTask Execute()
    {
        _mainGameUiModule.SwitchToBeforeGameView(_levelsService.LastSavedLevelNumber);
        _levelTrackModule.CreateLevelTrack(_levelsService.GetLevelData(_commandData.LevelNumber).LevelTack);
        
        await UniTaskHandler.WaitForAnyKeyPressed();
        
        _mainGameUiModule.SwitchToInGameView(_scoreModule.PlayerScore);
    }
}
