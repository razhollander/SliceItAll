using System.Collections;
using System.Collections.Generic;
using CoreDomain.Scripts.Utils.Command;
using UnityEngine;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.MainGameUi;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.PlayerSpaceship;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.Score;
using CoreDomain.Scripts.Utils.Command;
using CoreDomain.Services;
public class ResetGameCommand : CommandSync<ResetGameCommand>
{
    private readonly IFloorModule _floorModule;
    private readonly IAsteroidsModule _asteroidsModule;
    private readonly ITimePlayingModule _timePlayingModule;
    private readonly IHighScoreModule _highScoreModule;
    private readonly IScoreModule _scoreModule;
    private readonly IMainGameUiModule _mainGameUiModule;
    private readonly IPlayerSpaceshipModule _playerSpaceshipModule;
    private readonly ICameraService _cameraService;
    private readonly IGameSpeedService _gameSpeedService;

    public ResetGameCommand(
        IFloorModule floorModule,
        IAsteroidsModule asteroidsModule,
        ITimePlayingModule timePlayingModule,
        IHighScoreModule highScoreModule,
        IScoreModule scoreModule,
        IMainGameUiModule mainGameUiModule,
        IPlayerSpaceshipModule playerSpaceshipModule,
        ICameraService cameraService,
        IGameSpeedService gameSpeedService)
    {
        _floorModule = floorModule;
        _asteroidsModule = asteroidsModule;
        _timePlayingModule = timePlayingModule;
        _highScoreModule = highScoreModule;
        _scoreModule = scoreModule;
        _mainGameUiModule = mainGameUiModule;
        _playerSpaceshipModule = playerSpaceshipModule;
        _cameraService = cameraService;
        _gameSpeedService = gameSpeedService;
    }

    public override void Execute()
    {
        _gameSpeedService.Reset();
        _asteroidsModule.Reset();
        _scoreModule.ResetScore();
        _timePlayingModule.ResetTimer();
        _playerSpaceshipModule.ResetSpaceShip();
        _mainGameUiModule.HideGameOverPanel();
    }
}
