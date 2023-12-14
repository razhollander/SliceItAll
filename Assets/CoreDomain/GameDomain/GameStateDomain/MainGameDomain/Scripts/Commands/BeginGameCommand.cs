using System.Collections;
using System.Collections.Generic;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.GameKeyboardInputsModule;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.MainGameUi;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.PlayerSpaceship;
using CoreDomain.GameDomain.GameStateDomain.MainGameDomain.Modules.Score;
using CoreDomain.Scripts.Utils.Command;
using CoreDomain.Services;
using UnityEngine;

public class BeginGameCommand : CommandSync<BeginGameCommand>
{
    private readonly IFloorModule _floorModule;
    private readonly IAsteroidsModule _asteroidsModule;
    private readonly ITimePlayingModule _timePlayingModule;
    private readonly IHighScoreModule _highScoreModule;
    private readonly IScoreModule _scoreModule;
    private readonly IMainGameUiModule _mainGameUiModule;
    private readonly IPlayerSpaceshipModule _playerSpaceshipModule;
    private readonly ICameraService _cameraService;
    private readonly IGameInputActionsModule _gameInputActionsModule;

    public BeginGameCommand(
        IFloorModule floorModule,
        IAsteroidsModule asteroidsModule,
        ITimePlayingModule timePlayingModule,
        IHighScoreModule highScoreModule,
        IScoreModule scoreModule,
        IMainGameUiModule mainGameUiModule,
        IPlayerSpaceshipModule playerSpaceshipModule,
        ICameraService cameraService,
        IGameInputActionsModule gameInputActionsModule)
    {
        _floorModule = floorModule;
        _asteroidsModule = asteroidsModule;
        _timePlayingModule = timePlayingModule;
        _highScoreModule = highScoreModule;
        _scoreModule = scoreModule;
        _mainGameUiModule = mainGameUiModule;
        _playerSpaceshipModule = playerSpaceshipModule;
        _cameraService = cameraService;
        _gameInputActionsModule = gameInputActionsModule;
    }

    public override void Execute()
    {
        _gameInputActionsModule.EnableInputs();
        _floorModule.StartMovement();
        _asteroidsModule.StartSpawning();
        _scoreModule.StartCountingScore();
        _timePlayingModule.StartTimer();
        _highScoreModule.LoadLastHighScore();
        _playerSpaceshipModule.EnableSpaceShipMovement(true);
        _cameraService.SetCameraZoom(GameCameraType.World, true);

        _mainGameUiModule.SwitchToInGameView(_highScoreModule.LastHighScore, _scoreModule.PlayerScore, _timePlayingModule.TimePlaying,
            _asteroidsModule.AsteroidsPassedPlayerCounter);
    }
}
